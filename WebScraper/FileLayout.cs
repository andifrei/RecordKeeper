using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    /// <summary>
    /// Represents a basic flat file layout.
    /// </summary>
    public class FileLayout
    {
        #region Constructor
        /// <summary>
        /// Initializes the FileLayout
        /// </summary>
        /// <param name="rowdelimiter"></param>
        /// <param name="fielddelimiter"></param>
        /// <param name="firstrowheader"></param>
        public FileLayout(string rowdelimiter, string fielddelimiter, bool firstrowheader)
        {
            RowDelimiter = rowdelimiter;
            FieldDelimiter = fielddelimiter;
            FirstRowHeader = firstrowheader;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get's FieldDelimiter for rows
        /// </summary>
        public string FieldDelimiter
        {
            get;
            private set;
        }

        /// <summary>
        /// Get's the Row Delimiter
        /// </summary>
        public string RowDelimiter
        {
            get;
            private set;
        }

        /// <summary>
        /// Get's an indicator if the First Row is the Header Row
        /// </summary>
        public bool FirstRowHeader
        {
            get;
            private set;
        }
        #endregion

    }
}
