using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    /// <summary>
    /// Represents a row of a file
    /// </summary>
    public class FileRow
    {
        #region Constructor
        /// <summary>
        /// Initializes the File Row
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="parts"></param>
        public FileRow(string raw, IEnumerable<string> parts)
        {
            Parts = parts.ToList();
            Raw = raw;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get's the Parts of the file that makes up the row
        /// </summary>
        public List<string> Parts
        {
            get;
            private set;
        }

        /// <summary>
        /// Raw Text of the row
        /// </summary>
        public string Raw
        {
            get;
            private set;
        }
        #endregion

        #region Methods
        public string GetPartString(int index)
        {
            return Parts[index].Trim();
        }

        public decimal GetPartDecimal(int index)
        {
            return TryParseDecimal(Parts[index].Trim());
        }

        public int GetPartInt(int index)
        {
            return TryParseInt(Parts[index].Trim());
        }
        #endregion

        #region Helper
        public static int TryParseInt(string s)
        {
            int i = 0;
            int.TryParse(s, out i);
            return i;
        }

        public static decimal TryParseDecimal(string s)
        {
            decimal i = 0.0M;
            decimal.TryParse(s, out i);
            return i;
        }
        #endregion
    }
}
