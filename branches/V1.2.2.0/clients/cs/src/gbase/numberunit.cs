using System;
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.GoogleBase {

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Read-only base class for numbers with units.</summary>
    ///////////////////////////////////////////////////////////////////////
    public abstract class NumberUnit
    {
        /// <summary>unit name</summary>
        protected readonly string unit;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a NumberUnit object, leaving the responsability
        /// of deciding what the number is to subclasses.</summary>
        /// <param name="unit">unit name</param>
        ///////////////////////////////////////////////////////////////////////
        protected NumberUnit(string unit)
        {
            this.unit = unit;
        }

        /// <summary>The number as a float value.</summary>
        abstract protected float asFloat();

        /// <summary>The number as an integer value.</summary>
        abstract protected int asInt();

        ///////////////////////////////////////////////////////////////////////
        /// <summary>The unit</summary>
        ///////////////////////////////////////////////////////////////////////
        [CLSCompliant(false)]
        public string Unit
        {
            get
            {
                return unit;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>The value, as an integer. if the number is a float,
        /// the value will be rounded.</summary>
        ///////////////////////////////////////////////////////////////////////
        public int IntValue
        {
            get
            {
                return asInt();
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>The value, as a float.</summary>
        ///////////////////////////////////////////////////////////////////////
        public float FloatValue
        {
            get
            {
                return asFloat();
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the unit portion of a string, everything
        /// following the first space.</summary>
        ///////////////////////////////////////////////////////////////////////
        protected static string ExtractUnit(string str)
        {
            return str.Substring(FindSpace(str) + 1);
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the number portion of a string, everything
        /// preceding the first space.</summary>
        ///////////////////////////////////////////////////////////////////////
        protected static string ExtractNumber(string str)
        {
            return str.Substring(0, FindSpace(str));
        }

        private static int FindSpace(string str)
        {
            int space = str.IndexOf(' ');
            if (space == -1)
            {
                throw new FormatException("'" + str + "' lacks a unit");
            }
            return space;
        }
    }

    ///////////////////////////////////////////////////////////////////////
    /// <summary>A float with a unit.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class FloatUnit : NumberUnit
    {
        private readonly float number;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates a FloatUnit.</summary>
        /// <param name="number">the value</param>
        /// <param name="unit">the unit</param>
        ///////////////////////////////////////////////////////////////////////
        public FloatUnit(float number, String unit)
                : base(unit)
        {
            this.number = number;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Copy constructor.</summary>
        ///////////////////////////////////////////////////////////////////////
        public FloatUnit(NumberUnit orig)
                : base(orig.Unit)
        {
            this.number = orig.FloatValue;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Extracts the number and unit from a string
        /// of the form: <c>number " " unit</c></summary>
        /// <exception cref="FormatException">Thrown when the string
        /// representation could not be used to generate a valid
        /// FloatUnit.</exception>
        ///////////////////////////////////////////////////////////////////////
        public FloatUnit(String str)
                : base(ExtractUnit(str))
        {
            this.number = NumberFormat.ToFloat(ExtractNumber(str));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns a string representation for the FloatUnit that
        /// can be used to re-created another FloatUnit.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return NumberFormat.ToString(number) + " " + Unit;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value, as a float.</summary>
        ///////////////////////////////////////////////////////////////////////
        protected override float asFloat()
        {
            return number;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns the value, after converting it to an integer.
        /// </summary>
        ///////////////////////////////////////////////////////////////////////
        protected override int asInt()
        {
            return (int)number;
        }

        /// <summary>The value, as a float.</summary>
        public float Value {
            get
            {
                return number;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Two FloatUnit are equal if both their value and their
        /// units are equal. No unit conversion is ever done.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override bool Equals(object o)
        {
            if (Object.ReferenceEquals(this, o))
            {
                return true;
            }

            if (!(o is FloatUnit))
            {
                return false;
            }

            FloatUnit other = o as FloatUnit;
            return other.number == number && other.unit == unit;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates a hash code for this element that is
        /// consistent with its Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            return 49 * (19 + ((int)number)) + unit.GetHashCode();
        }
    }


    ///////////////////////////////////////////////////////////////////////
    /// <summary>An integer with a unit.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class IntUnit : NumberUnit
    {
        private readonly int number;

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Creates an IntUnit</summary>
        /// <param name="number">value</param>
        /// <param name="unit">unit, as a string</param>
        ///////////////////////////////////////////////////////////////////////
        public IntUnit(int number, String unit)
                : base(unit)
        {
            this.number = number;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Copy constructor.</summary>
        ///////////////////////////////////////////////////////////////////////
        public IntUnit(NumberUnit orig)
                : base(orig.Unit)
        {
            this.number = orig.IntValue;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Extracts the number and unit from a string
        /// of the form: <c>number " " unit</c></summary>
        /// <exception cref="FormatException">Thrown when the string
        /// representation could not be used to generate a valid
        /// IntUnit.</exception>
        ///////////////////////////////////////////////////////////////////////
        public IntUnit(string str)
                : base(ExtractUnit(str))
        {
            this.number = NumberFormat.ToInt(ExtractNumber(str));
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Returns a string representation for the FloatUnit that
        /// can be used to re-created another FloatUnit.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return NumberFormat.ToString(number) + " " + Unit;
        }

        /// <summary>Returns the value as a float.</summary>
        protected override float asFloat()
        {
            return number;
        }

        /// <summary>Returns the value.</summary>
        protected override int asInt()
        {
            return number;
        }

        /// <summary>The value, as an integer.</summary>
        public int Value {
            get
            {
                return number;
            }
        }


        ///////////////////////////////////////////////////////////////////////
        /// <summary>Two IntUnit are equal if both their value and their
        /// units are equal. No unit conversion is ever done. An IntUnit
        /// is never equal to a FloatUnit, even if they have the same
        /// value and unit.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override bool Equals(object o)
        {
            if (Object.ReferenceEquals(this, o))
            {
                return true;
            }


            if (!(o is IntUnit))
            {
                return false;
            }

            IntUnit other = o as IntUnit;
            return other.number == number && other.unit == unit;
        }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>Generates a hash code for this element that is
        /// consistent with its Equals() method.</summary>
        ///////////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            return 49 * (7 + number) + unit.GetHashCode();
        }

    }

}
