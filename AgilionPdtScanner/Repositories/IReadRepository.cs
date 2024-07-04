using AgilionPdtScanner.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilionPdtScanner.Repositories
{
    public interface IReadRepository
    {
        Task<List<OrderListDto>> GetShipmentOrderList();
        Task<List<Item>> GetShipmentOrderItems(string customerPhone);
    }
}
