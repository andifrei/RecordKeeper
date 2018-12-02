using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace RecordKeeper.Models
{
    public class RecordStoreViewModel
    {
        public List<RecordItem> RecordItems;
        public SelectList Stores;
        public string RecordStore { get; set; }
        public string SearchString { get; set; }
    }
}