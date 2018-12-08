using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    class RecklessRowGenerator : FileRowGenerator<RecklessHeaderRow, RecklessRow>
    {
        public override RecklessRow GenerateRow(string raw, List<string> parts, int index)
        {
            RecklessRow reckrow = new RecklessRow(raw, parts);
            return reckrow;
        }

        public override bool IsRowValid(string raw, List<string> parts)
        {

            return true;
        }

        public override RecklessHeaderRow GenerateHeaderRow(string raw, List<string> parts)
        {
            RecklessHeaderRow reckheadrow = new RecklessHeaderRow(raw, parts);
            return reckheadrow;
        }

        public override bool IsHeaderRowValid(string raw, List<string> parts)
        {
            return true;
        }

        public override List<string> ParseRow(string raw, string fielddelimiter)
        {
            List<string> parserow = new List<string>();
            parserow.AddRange(raw.Split(new[] { fielddelimiter }, StringSplitOptions.None));
            return parserow;
        }

        public override List<string> ParseHeader(string raw, string fielddelimiter)
        {
            List<string> parseheader = new List<string>();
            parseheader.AddRange(raw.Split(new[] { fielddelimiter }, StringSplitOptions.None));
            return parseheader;
        }
    }
}
