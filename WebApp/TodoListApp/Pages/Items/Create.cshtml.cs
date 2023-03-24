using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TodoListApp.Data;
using TodoListApp.Data.Remote;
using TodoListApp.Model;

namespace TodoListApp.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly TodoListAppContextWrapper _context;

        public CreateModel(TodoListAppContextWrapper context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ListEntry ListEntry { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var entries = await _context.ListEntries;

            if (!ModelState.IsValid || entries == null || ListEntry == null)
            {
                return Page();
            }

            await _context.Create(ListEntry);

            return RedirectToPage("./Index");
        }
    }
}
