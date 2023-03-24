using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using TodoListApp.Data.Remote;
using TodoListApp.Model;

namespace TodoListApp.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly TodoListAppContextWrapper _context;

        public IndexModel(TodoListAppContextWrapper context)
        {
            _context = context;
        }

        public IList<ListEntry> ListEntry { get;set; } = default!;

        public async Task OnGetAsync()
        {
              ListEntry = await _context.ListEntries;
        }
    }
}
