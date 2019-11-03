using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyScriptureJournal.Pages.MyJournal
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<JournalEntry> JournalEntry { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchNote { get; set; }
        
        public SelectList Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookName { get; set; }

        // Variables for sorting
        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // Use LINQ to get a list of books.
            IQueryable<string> bookQuery = from b in _context.JournalEntry
                                           orderby b.Book
                                           select b.Book;

            // Sorting options for the case statements below 
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            var books = from b in _context.JournalEntry
                        select b;

            if (!string.IsNullOrEmpty(SearchNote))
            {
                books = books.Where(s => s.Notes.Contains(SearchNote));
            }

            if (!string.IsNullOrEmpty(BookName))
            {
                books = books.Where(x => x.Book == BookName);
            }

            // Date and Book sorting
            switch (sortOrder)
            {
                case "Date":
                    books = books.OrderBy(s => s.EntryDate);
                    break;
                case "date_desc":
                    books = books.OrderByDescending(s => s.EntryDate);
                    break;
                case "book_desc":
                    books = books.OrderByDescending(s => s.Book);
                    break;
                default:
                    books = books.OrderBy(s => s.Book);
                    break;
            }

            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            JournalEntry = await books.ToListAsync();
        }
    }
}
