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
        public IList<SelectListItem> NodeIds { get; set; }
        public IList<SelectListItem> Types { get; set; } = new List<SelectListItem> {
            new SelectListItem { Value = "Temperature", Text = "Temperature" },
            new SelectListItem { Value = "Humidity", Text = "Humidity" }
        };

        [BindProperty]
        public string TargetType { get; set; }

        [BindProperty]
        public string TargetNodeId { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Nodes = await _context.Node.ToListAsync();
            Nodes = Nodes.DistinctBy(n => n.NodeId).ToList();
            NodeIds = Nodes.Select(n => new SelectListItem
            {
                Value = n.NodeId,
                Text = n.NodeId
            }).ToList();
        }

        public void OnPostCommand(string nodeId, string type, string value)
        {
            return;
        }
    }
}
