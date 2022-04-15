#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Listener.Models;

namespace MeshApp.Pages.Nodes
{
    public class IndexModel : PageModel
    {
        private readonly MeshAppContext _context;

        public IndexModel(MeshAppContext context)
        {
            _context = context;
        }

        public IList<Node> Nodes { get; set; }
        public async Task OnGetAsync()
        {
            await GetData();
        }
        private async Task GetData()
        {
            Nodes = await _context.Node.ToListAsync();
            Nodes = Nodes.DistinctBy(n => n.NodeId).ToList();
        }
    }
}
