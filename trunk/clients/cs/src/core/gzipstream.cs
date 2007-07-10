using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Google.GData.Client
{
    public enum CompressionMode
	{
        Compress,
        Decompress,
	}

    public class GZipStream : Stream
    {
        #region Native const, structs, and defs
        private const string ZLibVersion = "1.2.3";

        private enum ZLibReturnCode
        {
            Ok = 0,
            StreamEnd = 1,
            NeedDictionary = 2,
            Errno = -1,
            StreamError = -2,
            DataError = -3,
            MemoryError = -4,
            BufferError = -5,
            VersionError = -6
        }

        private enum ZLibFlush
        {
            NoFlush = 0,
            PartialFlush = 1,
            SyncFlush = 2,
            FullFlush = 3,
            Finish = 4
        }

        private enum ZLibCompressionLevel
        {
            NoCompression = 0,
            BestSpeed = 1,
            BestCompression = 2,
            DefaultCompression = 3
        }

        private enum ZLibCompressionStrategy
        {
            Filtered = 1,
            HuffmanOnly = 2,
            DefaultStrategy = 0
        }

        private enum ZLibCompressionMethod
        {
            Delated = 8
        }

        private enum ZLibDataType
        {
            Binary = 0,
            Ascii = 1,
            Unknown = 2,
        }

        private enum ZLibOpenType
        {
            ZLib = 15,
            GZip = 15 + 16,
            Both = 15 + 32,
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        private struct z_stream
        {
            public IntPtr next_in;  /* next input byte */
            public uint avail_in;  /* number of bytes available at next_in */
            public uint total_in;  /* total nb of input bytes read so far */

            public IntPtr next_out; /* next output byte should be put there */
            public uint avail_out; /* remaining free space at next_out */
            public uint total_out; /* total nb of bytes output so far */

            public IntPtr msg;      /* last error message, NULL if no error */
            public IntPtr state; /* not visible by applications */

            public IntPtr zalloc;  /* used to allocate the internal state */
            public IntPtr zfree;   /* used to free the internal state */
            public IntPtr opaque;  /* private data object passed to zalloc and zfree */

            public ZLibDataType data_type;  /* best guess about the data type: ascii or binary */
            public uint adler;      /* adler32 value of the uncompressed data */
            public uint reserved;   /* reserved for future use */
        };
        #endregion

        #region P/Invoke
#if WindowsCE || PocketPC
        [DllImport("zlib.arm.dll", EntryPoint = "inflateInit2_", CharSet = CharSet.Auto)]
#else
        [DllImport("zlib.x86.dll", EntryPoint = "inflateInit2_", CharSet = CharSet.Ansi)]
#endif
        private static extern ZLibReturnCode    inflateInit2(ref z_stream strm, ZLibOpenType windowBits, string version, int stream_size);

#if WindowsCE || PocketPC
        [DllImport("zlib.arm.dll", CharSet = CharSet.Auto)]
#else
        [DllImport("zlib.x86.dll", CharSet = CharSet.Ansi)]
#endif
        private static extern ZLibReturnCode     inflate(ref z_stream strm, ZLibFlush flush);

#if WindowsCE || PocketPC
        [DllImport("zlib.arm.dll", CharSet = CharSet.Auto)]
#else
        [DllImport("zlib.x86.dll", CharSet = CharSet.Ansi)]
#endif
        private static extern ZLibReturnCode    inflateEnd(ref z_stream strm);
        #endregion

        private const int           BufferSize = 16384;

        private Stream              compressedStream;
        private CompressionMode     mode;

        private z_stream            zstream = new z_stream();

        private byte[]              inputBuffer = new byte[BufferSize];
        private GCHandle            inputBufferHandle;

        public GZipStream(Stream stream, CompressionMode mode)
        {
            this.compressedStream = stream;
            this.mode = mode;

            this.inputBufferHandle = GCHandle.Alloc(inputBuffer, GCHandleType.Pinned);

            this.zstream.zalloc = IntPtr.Zero;
            this.zstream.zfree = IntPtr.Zero;
            this.zstream.opaque = IntPtr.Zero;

            inflateInit2(ref this.zstream, ZLibOpenType.Both, ZLibVersion, Marshal.SizeOf(typeof(z_stream)));
        }

        ~GZipStream()
        {
            inputBufferHandle.Free();
            inflateEnd(ref this.zstream);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.mode == CompressionMode.Compress)
                throw new NotSupportedException("Can't read on a compress stream!");

            bool exitLoop = false;

            byte[] tmpOutputBuffer = new byte[count];
            GCHandle tmpOutpuBufferHandle = GCHandle.Alloc(tmpOutputBuffer, GCHandleType.Pinned);

            this.zstream.next_out = tmpOutpuBufferHandle.AddrOfPinnedObject();
            this.zstream.avail_out = (uint)tmpOutputBuffer.Length;

            try
            {
                while (this.zstream.avail_out > 0 && exitLoop == false)
                {
                    if (this.zstream.avail_in == 0)
                    {
                        int readLength = this.compressedStream.Read(inputBuffer, 0, inputBuffer.Length);
                        this.zstream.avail_in = (uint)readLength;
                        this.zstream.next_in = this.inputBufferHandle.AddrOfPinnedObject();
                    }
                    ZLibReturnCode  result = inflate(ref zstream, ZLibFlush.NoFlush);
                    switch (result)
                    {
                        case ZLibReturnCode.StreamEnd:
                            exitLoop = true;
                            Array.Copy(tmpOutputBuffer, 0, buffer, offset, count - (int)this.zstream.avail_out);
                            break;
                        case ZLibReturnCode.Ok:
                            Array.Copy(tmpOutputBuffer, 0, buffer, offset, count - (int)this.zstream.avail_out);
                            break;
                        case ZLibReturnCode.MemoryError:
                            throw new OutOfMemoryException();
                        default:
                            throw new Exception("Zlib Return Code: " + result.ToString());
                    }
                }

                return (count - (int)this.zstream.avail_out);
            }
            finally
            {
                tmpOutpuBufferHandle.Free();
            }
        }

        public override void Close()
        {
            this.compressedStream.Close();
            base.Close();
        }

        public override bool CanRead
        {
            get { return (this.mode == CompressionMode.Decompress ? true : false); }
        }

        public override bool CanWrite
        {
            get { return (this.mode == CompressionMode.Compress ? true : false); }
        }

        public override bool CanSeek
        {
            get { return (false); }
        }

        public Stream BaseStream
        {
            get { return (this.compressedStream); }
        }

        #region Not yet supported
        public override void Flush()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Not yet supported!");
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        #endregion
    }
}