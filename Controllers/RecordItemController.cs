using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecordKeeper.Models;
using AngleSharp;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace RecordKeeper.Controllers
{
    public class RecordItemController : Controller
    {
        private readonly RecordKeeperContext _context;

        public RecordItemController(RecordKeeperContext context)
        {
            _context = context;
        }

        // GET: RecordItem
        public async Task<IActionResult> Index(string recordStore, string searchString)
        {
            //var recordItems = new List<RecordItem>{};
            //foreach(RecordItem i in _context.RecordItem)
            //{
            //    recordItems.Add(i);
            //}
            //Get stores from DB
            IQueryable<string> storeQuery = from r in _context.RecordItem 
                                                orderby r.Store
                                                select r.Store;

            var recordItems = from r in _context.RecordItem select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                //var test = recordItems.Where(i => i.Artist.Contains(searchString));
                recordItems = recordItems.Where(i => i.Artist.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(recordStore))
            {
                recordItems = recordItems.Where(i => i.Store == recordStore);
            }

            var recordStoreVM = new RecordStoreViewModel();
            recordStoreVM.Stores = new SelectList(await storeQuery.Distinct().ToListAsync());
            recordStoreVM.RecordItems = await recordItems.ToListAsync();
            recordStoreVM.SearchString = searchString;

            return View(recordStoreVM);

            //return View(await recordItems.ToListAsync());
            //return View(await recordItems.ToAsyncEnumerable());
        }

        public async Task<IActionResult> AlbumSearch(string searchString)
        {
            //var recordItems = new List<RecordItem>{};
            //foreach(RecordItem i in _context.RecordItem)
            //{
            //    recordItems.Add(i);
            //}
            var recordItems = from r in _context.RecordItem select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                //var test = recordItems.Where(i => i.Artist.Contains(searchString));
                recordItems = recordItems.Where(i => i.Album.Contains(searchString));
            }

            return View(await recordItems.ToListAsync());
            //return View(await recordItems.ToAsyncEnumerable());
        }

        // GET: RecordItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordItem = await _context.RecordItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recordItem == null)
            {
                return NotFound();
            }

            return View(recordItem);
        }

        // GET: RecordItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecordItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Artist,Album,Label,Description,StoreLocation,Type,Price,AsOf,Store, UserID")] RecordItem recordItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recordItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recordItem);
        }

        // GET: RecordItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordItem = await _context.RecordItem.FindAsync(id);
            if (recordItem == null)
            {
                return NotFound();
            }
            return View(recordItem);
        }

        // POST: RecordItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Artist,Album,Label,Description,StoreLocation,Type,Price,AsOf,Store,UserID")] RecordItem recordItem)
        {
            if (id != recordItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recordItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordItemExists(recordItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recordItem);
        }

        // GET: RecordItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordItem = await _context.RecordItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recordItem == null)
            {
                return NotFound();
            }

            return View(recordItem);
        }

        // POST: RecordItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recordItem = await _context.RecordItem.FindAsync(id);
            _context.RecordItem.Remove(recordItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordItemExists(int id)
        {
            return _context.RecordItem.Any(e => e.ID == id);
        }

        // GET: RecordItem/PriceUpdate/6
        public async Task<IActionResult> PriceUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recordItem = await _context.RecordItem.FindAsync(id);
            string test = recordItem.Artist + " " + recordItem.Album;
            var result = MainAsync(test);
            result.Wait();
            
            if (recordItem == null)
            {
                return NotFound();
            }
            return View(recordItem);
        }


        // POST: RecordItem/PriceUpdate/6
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PriceUpdate(int id, [Bind("ID,Artist,Album,Label,Description,StoreLocation,Type,Price,AsOf,Store,UserID")] RecordItem recordItem)
        {
            if (id != recordItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recordItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordItemExists(recordItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recordItem);
        }



        static async Task<List<RecordItem>> MainAsync(string search)
        {
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.reckless.com/";
            Dictionary<string, string> fields = new Dictionary<string, string> {
                { "keywords", search },
                { "format", ""},
                { "store", "" },
                { "cond", "" }
            };

            var context = BrowsingContext.New(config);
            await context.OpenAsync(address);
            var form = context.Active.QuerySelector("form") as IHtmlFormElement;
            
            var document = await form.SetValues((fields)).SubmitAsync(context.Active.QuerySelector("srch"));
            // Asynchronously get the document in a new context using the configuration


            var cells = document.QuerySelectorAll("td.main table");
            List<string> result = new List<string>();
            foreach (var item in cells)
            {
                var tableRows = item.QuerySelectorAll(">tr >td").Select(m => m.Text()).ToList();
                for (int i = 0; i < tableRows.Count(); i++)
                {
                    tableRows[i] = Regex.Replace(tableRows[i], @"\t|\n|\r", "");
                }
                var row = String.Join("\t", tableRows.ToArray());
                result.Add(row);
            }
            
            var what = String.Join("\n", result.ToArray());


            ParsedFile<RecklessHeaderRow, RecklessRow> parsed = new ParsedFile<RecklessHeaderRow, RecklessRow>();
            parsed = RecklessParser.Parse(what);
            List<RecordItem> records = RecklessParser.GenerateReckless(parsed);


            return records;
        }
    }
}
