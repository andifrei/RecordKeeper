using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    class RecklessParser
    {
        public static ParsedFile<RecklessHeaderRow, RecklessRow> Parse(string dest)
        {
            FileLayout layout = new FileLayout("\n", "\t", false);
            FileParser<RecklessHeaderRow, RecklessRow> parser = new FileParser<RecklessHeaderRow, RecklessRow>(layout, dest, new RecklessRowGenerator());

            ///////////////////////////////////////////
            //Read the number of columns to decide 
            //if we should proceed
            /////////////////////////////////////////
            int numberofcolumnsinfile = parser.GetNumberOfColumns();

            /////////////////////////////////////////////////
            //Check if the file has expected columns 
            //only then proceed else throw an exception
            ////////////////////////////////////////////////

            ParsedFile<RecklessHeaderRow, RecklessRow> parsed = parser.Parse();
            return parsed;
        }

        public static List<Models.RecordItem> GenerateReckless(ParsedFile<RecklessHeaderRow, RecklessRow> parsed)
        {
            

            List<Models.RecordItem> holder = new List<Models.RecordItem>();
            foreach (RecklessRow row in parsed.Rows)
            {
                Models.RecordItem reckl = new Models.RecordItem();
                if(row.Description.Length < 50)
                {
                    try
                    {
                        reckl.Artist = row.ArtistAlbum;
                        reckl.Album = row.ArtistAlbum;
                        reckl.Label = row.Label;
                        reckl.Description = "";
                        reckl.StoreLocation = row.Description;
                        reckl.Type = row.StoreLocation.Split(new[] { "$" }, StringSplitOptions.None)[0];
                        reckl.Price = FileRow.TryParseDecimal(row.StoreLocation.Split(new[] { "$" }, StringSplitOptions.None)[1]);

                        holder.Add(reckl);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    try
                    {
                        reckl.Artist = row.ArtistAlbum;
                        reckl.Album = row.ArtistAlbum;
                        reckl.Label = row.Label;
                        reckl.Description = row.Description;
                        reckl.StoreLocation = row.StoreLocation;
                        reckl.Type = row.TypePrice.Split(new[] { "$" }, StringSplitOptions.None)[0];
                        reckl.Price = FileRow.TryParseDecimal(row.TypePrice.Split(new[] { "$" }, StringSplitOptions.None)[1]);

                        holder.Add(reckl);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return holder;
        }
    }
}
