using System;
using System.Collections.Generic;

namespace DotnetCoding.Infrastructure
{
    public partial class QueueApproval
    {
        public long QueueId { get; set; }
        public long ProductId { get; set; }
        public DateTime RequestedDate { get; set; }
        public string RequestedReason { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int RequestedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
    }
}
