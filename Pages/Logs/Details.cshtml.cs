#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Listener.Models;

namespace MeshApp.Pages.Logs
{
    public class DetailsModel : PageModel
    {
        private readonly MeshAppContext _context;

        public DetailsModel(MeshAppContext context)
        {
            _context = context;
        }

        public Log Log { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Log = await _context.Log.FirstOrDefaultAsync(m => m.Id == id);

            if (Log == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
