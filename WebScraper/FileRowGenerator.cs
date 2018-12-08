using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public abstract class FileRowGenerator<THeaderRow, TRow> : IFileRowGenerator<THeaderRow, TRow>
        where THeaderRow : FileRow
        where TRow : FileRow
    {

        public abstract TRow GenerateRow(string raw, List<string> parts, int index);

        public abstract bool IsRowValid(string raw, List<string> parts);

        public abstract THeaderRow GenerateHeaderRow(string raw, List<string> parts);

        public abstract bool IsHeaderRowValid(string raw, List<string> parts);

        public abstract List<string> ParseRow(string raw, string fielddelimiter);

        public abstract List<string> ParseHeader(string raw, string fielddelimiter);
    }
}
