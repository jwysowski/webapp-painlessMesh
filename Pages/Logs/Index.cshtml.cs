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
    public class IndexModel : PageModel
    {
        private readonly MeshAppContext _context;

        public IndexModel(MeshAppContext context)
        {
            _context = context;
        }
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public IList<Log> Logs { get; set; }

        public async Task OnGetAsync(int currentPage = 1)
        {
            CurrentPage = currentPage;
            Logs = await _context.Log.OrderByDescending(l => l.Id)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Count = await _context.Log.CountAsync();
        }
    }
}
