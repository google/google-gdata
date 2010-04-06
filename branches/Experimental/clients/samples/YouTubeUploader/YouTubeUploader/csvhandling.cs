using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using LumenWorks.Framework.IO.Csv;



/// this file contains the CSV related support code

namespace YouTubeUploader
{
    public partial class YouTubeUploader : Form
    {

        private void OpenCSVFile_Click(object sender, EventArgs e)
        {
            if (this.chooseCSVFile.ShowDialog() == DialogResult.OK)
            {
                LoadCSVFile(this.chooseCSVFile.FileName);
            }
        }

        private void LoadCSVFile(String filename)
        {
            this.csvDisplayGrid.DataSource = null;
            this.csvDisplayGrid.Columns.Clear();
            this.csvDisplayGrid.Rows.Clear();

            lock (this.flag)
            {
                this.queue = new List<UserState>();
                this.retryQueue = new List<UserState>();
            }


            Stream f = File.Open(filename, FileMode.Open);
            using (CachedCsvReader csv = new CachedCsvReader(new StreamReader(f, Encoding.UTF8), true))
            {
                this.csvDisplayGrid.DataSource = csv;
                while (this.csvDisplayGrid.Columns.Count > COLUMN_MAX)
                {
                    this.csvDisplayGrid.Columns.RemoveAt(6);
                }
                if (this.csvDisplayGrid.Columns.Count != COLUMN_MAX)
                {
                    MessageBox.Show("The file " + filename + " is not in the correct format");
                    this.csvDisplayGrid.DataSource = null;
                }
            }


            // now add a status column
            DataGridViewColumn status = new DataGridViewColumn();
            status.Name = "Upload Status";
            status.CellTemplate = new DataGridViewTextBoxCell();
            this.csvDisplayGrid.Columns.Add(status);

            DataGridViewColumn vid = new DataGridViewColumn();
            vid.Name = "Video ID";
            vid.CellTemplate = new DataGridViewTextBoxCell();
            this.csvDisplayGrid.Columns.Add(vid);

            this.Upload.Enabled = true;

            SetTitle();

            Trace.TraceInformation("Loaded csv file: " + filename + "successfully");

        }


        private void ChooseOutputFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.saveOutput.ShowDialog() == DialogResult.OK)
                {
                    SaveGridAsCSV(this.saveOutput.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem saving your output file: ", ex.Message);
            }
        }

        private void SaveGridAsCSV(String filename)
        {

            Stream file = File.Open(filename, FileMode.Create);

            StreamWriter writer = new StreamWriter(file);
            // iterate over the contents, and create a CSV file
            int counter = 0;
            foreach (DataGridViewColumn col in this.csvDisplayGrid.Columns)
            {
                OutputCell(writer, col.Name, ref counter);
            }
            writer.WriteLine();

            foreach (DataGridViewRow row in this.csvDisplayGrid.Rows)
            {
                counter = 0;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                        OutputCell(writer, cell.Value.ToString(), ref counter);
                }
                writer.WriteLine();
            }
            writer.Flush();
            writer.Close();
        }

        private void OutputCell(StreamWriter writer, string value, ref int index)
        {
            if (index > 0)
            {
                writer.Write(",");
            }
            writer.Write("\"" + value + "\"");
            index++;
        }

    }
}
