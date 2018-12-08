using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public interface IFileRowGenerator<THeaderRow, TRow>
        where THeaderRow : FileRow
        where TRow : FileRow
    {
        /// <summary>
        /// Parses the row
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        List<string> ParseRow(string raw, string fielddelimiter);
        /// <summary>
        /// Generate's the Row
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        TRow GenerateRow(string raw, List<string> parts, int index);

        /// <summary>
        /// Checks if the Row is Correctly formatted
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        bool IsRowValid(string raw, List<string> parts);

        /// <summary>
        /// Parses the Header
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        List<string> ParseHeader(string raw, string fielddelimiter);
        /// <summary>
        /// Generates the header row
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        THeaderRow GenerateHeaderRow(string raw, List<string> parts);

        /// <summary>
        /// Validates if the HeaderRow is correct
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        bool IsHeaderRowValid(string raw, List<string> parts);
    }
}
