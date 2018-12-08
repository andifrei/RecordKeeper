using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    /// <summary>
    /// The results of parsing a file
    /// </summary>
    /// <typeparam name="THeaderRow"></typeparam>
    /// <typeparam name="TRow"></typeparam>
    public class ParsedFile<THeaderRow, TRow>
        where THeaderRow : FileRow
        where TRow : FileRow
    {

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ParsedFile()
        {
            Rows = new List<TRow>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get's Set's the Header Row
        /// </summary>
        public THeaderRow Header
        {
            get;
            set;
        }

        /// <summary>
        /// Get's all of the rows
        /// </summary>
        public List<TRow> Rows
        {
            get;
            private set;
        }

        #endregion

    }
}