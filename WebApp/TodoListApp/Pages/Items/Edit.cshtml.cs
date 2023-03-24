using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using TodoListApp.Data.Remote;
using TodoListApp.Model;

namespace TodoListApp.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly TodoListAppContextWrapper _context;



        public EditModel(TodoListAppContextWrapper context)
        {
            _context = context;
        }

        [BindProperty]
        public ListEntry ListEntry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ListEntries == null)
            {
                return NotFound();
            }

            var listentry =  (await _context.ListEntries).FirstOrDefault(m => m.ID == id);
            if (listentry == null)
            {
                return NotFound();
            }
            ListEntry = listentry;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var success = await _context.Attach(ListEntry);

            if(!success)
            {
                NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
