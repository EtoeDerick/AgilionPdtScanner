using AgilionPdtScanner.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace AgilionPdtScanner.Repositories
{
    public class MockDataRepository : IReadRepository
    {
        
        public async Task<List<OrderListDto>> GetShipmentOrderList()
        {
            List<OrderListDto>? orderListDtos = new List<OrderListDto>();

            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Bordereaux.json");
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                var rootObject = JsonConvert.DeserializeObject<RootObject>(contents);
                if (rootObject?.PackingSlips != null)
                {
                    foreach (var packingSlip in rootObject.PackingSlips)
                    {
                        var order = new OrderListDto
                        {
                            Number = packingSlip.Number,
                            Name = packingSlip.Name,
                            Date = packingSlip.Date,
                            Phonenumber = packingSlip.Phone,
                            ItemsCount = packingSlip.Items.Count()
                        };
                        
                        orderListDtos.Add(order);
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            return orderListDtos;
        }
        public async Task<List<Item>> GetShipmentOrderItems(string customerPhone)
        {
            List<Item> orderItems = new List<Item>();

            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Bordereaux.json");
                using var reader = new StreamReader(stream);
                var contents = await reader.ReadToEndAsync();
                var rootObject = JsonConvert.DeserializeObject<RootObject>(contents);
                if (rootObject?.PackingSlips != null)
                {
                    foreach (var packingSlip in rootObject.PackingSlips)
                    {
                        if(packingSlip.Phone == customerPhone && packingSlip.Items != null)
                        {
                            foreach (var item in packingSlip.Items)
                            {
                                var newItem = new Item()
                                {
                                    Code = item.Code,
                                    Quantity = item.Quantity,
                                    Designation = item.Designation,
                                    StatusColor = item.StatusColor,
                                };
                                orderItems.Add(newItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return orderItems;
        }

    }
}
