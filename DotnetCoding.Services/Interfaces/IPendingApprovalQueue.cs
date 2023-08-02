using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Interfaces
{
    internal interface IPendingApprovalQueue : IGenericRepository<PendingApprovalQueue>
    {
    }
}
