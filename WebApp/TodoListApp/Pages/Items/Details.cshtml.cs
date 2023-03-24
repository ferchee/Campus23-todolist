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
    public class DetailsModel : PageModel
    {
        private readonly TodoListAppContextWrapper _context;

        public DetailsModel(TodoListAppContextWrapper context)
        {
            _context = context;
        }

      public ListEntry ListEntry { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var entries = await _context.ListEntries;

            if (id == null || entries == null)
            {
                return NotFound();
            }

            var listentry = entries.FirstOrDefault(m => m.ID == id);
            if (listentry == null)
            {
                return NotFound();
            }
            else 
            {
                ListEntry = listentry;
            }
            return Page();
        }
    }
}
