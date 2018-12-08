using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class RecklessRow : FileRow
    {
        #region Fields
        private class FieldIDs
        {
            public const int ArtistAlbum = 0;
            public const int Label = 1;
            public const int Description = 2;
            public const int StoreLocation = 3;
            public const int TypePrice = 4;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="parts"></param>
        public RecklessRow(string raw, List<string> parts) : base(raw, parts) { }
        #endregion

        #region Properties
        public string ArtistAlbum
        {
            get { return GetPartString(FieldIDs.ArtistAlbum); }
        }
        public string Label
        {
            get { return GetPartString(FieldIDs.Label); }
        }
        public string Description
        {
            get { return GetPartString(FieldIDs.Description); }
        }
        public string StoreLocation
        {
            get { return GetPartString(FieldIDs.StoreLocation); }
        }
        public string TypePrice
        {
            get { return GetPartString(FieldIDs.TypePrice); }
        }
        #endregion
    }
}
