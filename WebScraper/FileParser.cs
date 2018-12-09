using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    /// <summary>
    /// Basic File Parser for files that are Row \ Field Delimited
    /// </summary>
    public class FileParser<THeaderRow, TRow>
        where THeaderRow : FileRow
        where TRow : FileRow
    {

        #region Constructor
        /// <summary>
        /// Initializes the FileParser
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="path"></param>
        /// <param name="generator"></param>
        public FileParser(FileLayout layout, string path, IFileRowGenerator<THeaderRow, TRow> generator)
        {
            RowGenerator = generator;
            Layout = layout;
            Path = path;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get's the Path to file to import
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        /// <summary>
        /// Get's the File Row Generator to correctly figure out what type of row it is
        /// </summary>
        public IFileRowGenerator<THeaderRow, TRow> RowGenerator
        {
            get;
            private set;
        }

        /// <summary>
        /// Get's the File Layout
        /// </summary>
        public FileLayout Layout
        {
            get;
            private set;
        }
        #endregion

        #region Row Parsing
        /// <summary>
        /// Parses just the header
        /// </summary>
        /// <returns></returns>
        public ParsedFile<THeaderRow, TRow> ParseHeader()
        {
            var parsed = new ParsedFile<THeaderRow, TRow>();
            var rows = ParseRows();
            var header = rows[0];
            List<string> fields = ParseRow(header);
            parsed.Header = GenerateHeaderRow(fields, header);
            return parsed;

        }



        public ParsedFile<THeaderRow, TRow> Parse(string content)
        {
            ////////////////////////////////////////////
            //Parse out the rows
            ////////////////////////////////////////////
            var rows = new List<string>(content.Split(Layout.RowDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            return InternalParse(rows);
        }


        /// <summary>
        /// Parses the file based on the layout
        /// </summary>
        /// <returns>Parsed File</returns>
        public ParsedFile<THeaderRow, TRow> Parse()
        {

            ////////////////////////////////////////////
            //Parse out the rows
            ////////////////////////////////////////////
            var rows = ParseRows();
            return InternalParse(rows);
        }

        private ParsedFile<THeaderRow, TRow> InternalParse(List<string> rows)
        {

            ////////////////////////////////////////////
            //Create the parsed file
            ////////////////////////////////////////////
            var parsed = new ParsedFile<THeaderRow, TRow>();
            ////////////////////////////////////////////
            //iterate the rows and generate the correct
            //output of a parts file
            ////////////////////////////////////////////
            for (int i = 0; i < rows.Count; i++)
            {
                ////////////////////////////////////////////
                //Parse the row into a list of string
                ////////////////////////////////////////////
                string row = rows[i];
                List<string> fields = null;

                ////////////////////////////////////////////
                //The Layout indicates that the first row
                //is the header row
                ////////////////////////////////////////////
                if (i == 0 && Layout.FirstRowHeader)
                {
                    fields = ParseHeaderRow(row);
                    parsed.Header = GenerateHeaderRow(fields, row);
                    continue;
                }
                fields = ParseRow(row);
                parsed.Rows.Add(GenerateRow(fields, row, i));
            }
            return parsed;
        }

        /// <summary>
        /// Generate a Row from the fields and raw text
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="raw"></param>
        /// <returns></returns>
        private TRow GenerateRow(List<string> fields, string raw, int index)
        {
            if (!RowGenerator.IsRowValid(raw, fields))
            {
                throw new Exception("Invalid Row {0}: {1}");
            }
            else
            {
                return RowGenerator.GenerateRow(raw, fields, index);
            }
        }

        /// <summary>
        /// Generates the HeaderRow from the fields and raw text
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="raw"></param>
        /// <returns></returns>
        private THeaderRow GenerateHeaderRow(List<string> fields, string raw)
        {
            if (!RowGenerator.IsHeaderRowValid(raw, fields))
            {
                throw new Exception("Invalid Header Row :" + raw);
            }
            else
            {
                return RowGenerator.GenerateHeaderRow(raw, fields);
            }
        }


        /// <summary>
        /// Parse's a HeaderRow from the raw text
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        protected virtual List<string> ParseHeaderRow(string raw)
        {
            /////////////////////////////////////////////
            //Basic string.split using the field delimiter
            /////////////////////////////////////////////
            return RowGenerator.ParseHeader(raw, Layout.FieldDelimiter);
        }

        /// <summary>
        /// Parse's a Row from the raw text
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        protected virtual List<string> ParseRow(string raw)
        {
            /////////////////////////////////////////////
            //Basic string.split using the field delimiter
            /////////////////////////////////////////////
            return RowGenerator.ParseRow(raw, Layout.FieldDelimiter);
        }

        /// <summary>
        /// Basic Parsing Splitting on RowDelimiter.
        /// in the future if this fails can come up with a factory method using the decorator pattern to handle this
        /// </summary>
        /// <returns></returns>
        protected virtual List<string> ParseRows()
        {
            List<string> rows = null;
            /////////////////////////////////////////////
            //if the row delimiter is LineFeed \n
            //the readall lines should work.
            /////////////////////////////////////////////
            //if (Layout.RowDelimiter == "\n")
            //{
            //    /////////////////////////////////////////////
            //    //RowDelimiter is line feed read all lines works!
            //    /////////////////////////////////////////////
            //    rows = new List<string>(System.IO.File.ReadAllLines(Path));
            //}
            //else
            //{
                /////////////////////////////////////////////
                //Non line fied delimiter read all of the text
                //and do a string.split
                /////////////////////////////////////////////
                //string text = System.IO.File.ReadAllText(Path);
                rows = new List<string>(Path.Split(Layout.RowDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            //}
            return rows;
        }

        #endregion


        /// <summary>
        /// Pari: Adding this to read number of columns for Transcore Rate Index validation
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetNumberOfColumns()
        {
            List<string> rows = ParseRows();
            if(rows.Count == 0) return 0;
            return rows[0].Split(',').Length;
        }
    }
}