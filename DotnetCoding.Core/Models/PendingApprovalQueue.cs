using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Models
{
    public class PendingApprovalQueue
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsActive { get; set; }
        public string? RequestedReason { get; set; }
        public DateTime RequestedDate { get; set; }

    }
}
