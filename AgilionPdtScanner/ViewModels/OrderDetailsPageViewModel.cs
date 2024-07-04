using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AgilionPdtScanner.DTOs;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using AgilionPdtScanner.Repositories;

namespace AgilionPdtScanner.ViewModels
{
    public class OrderDetailsPageViewModel : BaseViewModel
    {
        private OrderListDto _selectedOrder;
        private readonly IReadRepository _db;
        private ObservableCollection<Item>? _orderListDtos;
        public OrderListDto SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));                
            }
        }

        public ObservableCollection<Item>? OrderListDtos 
        { get => _orderListDtos;
            set
            {
                _orderListDtos = value;
                OnPropertyChanged(nameof(OrderListDtos));
            } 
        }
        public IAsyncRelayCommand<OrderListDto> LoadOrderListCommand { get; }
        public IRelayCommand<Item> ItemClickedCommand { get; }

        public OrderDetailsPageViewModel()
        {
            IsBusy = true;
            _orderListDtos = new ObservableCollection<Item>();
        }

        public OrderDetailsPageViewModel(IReadRepository db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            IsBusy = true;
            _orderListDtos = new ObservableCollection<Item>();
            LoadOrderListCommand = new AsyncRelayCommand<OrderListDto>(ExecuteLoadOrderListCommand);
            ItemClickedCommand = new RelayCommand<Item>(ExecuteItemClickedCommand);
        }

        public async Task ExecuteLoadOrderListCommand(OrderListDto orderItem)
        {
            int count = 1;
            try
            {
                var orders = await _db.GetShipmentOrderItems(orderItem.Phonenumber); // Replace with your method to get orders
                if (OrderListDtos != null )
                {
                    OrderListDtos.Clear();

                    foreach (var order in orders)
                    {
                        order.Numbering = count++;
                        OrderListDtos.Add(order);
                    }
                }
                
            }
            catch (Exception ex)
            {
                //await _messageService.ShowAsync("Error", ex.Message); // Handle exceptions
            }
            
        }

        public void ExecuteItemClickedCommand(Item item) 
        {
            ExecuteItemScannedCommand(item.Code);
            /*int index = 0;
            string statusColor = "Green";
            //Update properties found in SelectedOrder Item
            //1. Check if the product scanned is found in the list of items
            //2. Update respectives UI

            //2.1 Change the title of the successfully scanned item to green
            foreach (var productItem in OrderListDtos)
            {
                if (productItem.Code == item.Code && productItem.StatusColor.ToLower() == statusColor.ToLower())
                {
                    OrderListDtos.ElementAt(index).IsBusy = true;
                    //This item has been scanned already
                    //Notify the user and return
                    Shell.Current.DisplayAlert("No duplicate scan allowed", "This item has been scanned already", "OK");
                    OrderListDtos.ElementAt(index).IsBusy = false;
                    return;
                }
                else if (productItem.Code == item.Code)
                {
                    OrderListDtos.ElementAt(index).StatusColor = statusColor;
                    OrderListDtos.ElementAt(index++).IsBusy = true;
                                     
                    break;
                }
                index++;
            }

            //2.2 Update the percentage for boxview for each successfully scanned item
            float quantity = SelectedOrder.QuantityScanned + 1;
            float totalItems = SelectedOrder.ItemsCount;
            float percentage = (quantity / totalItems);

            int boxviewPercentageLength = (int)(percentage*300);

            if (quantity == SelectedOrder.ItemsCount) 
            {
                boxviewPercentageLength = 298;
            }

            SelectedOrder.QuantityScanned = quantity;            
            SelectedOrder.QuantityScannedPercentage = boxviewPercentageLength; //280 is the total length of the boxview

            if (index > 0 ) 
            {
                OrderListDtos.ElementAt(index - 1).IsBusy = false;
            }
            else
            {
                OrderListDtos.ElementAt(index).IsBusy = false;
            }*/
        }

        public void ExecuteItemScannedCommand(string itemBarcodeValue)
        {
            int index = 0;
            string statusColor = "Green";

            bool itemCodeIsFound = false;

            //Update properties found in SelectedOrder Item
            //1. Check if the product scanned is found in the list of items
            //2. Update respectives UI

            //2.1 Change the title of the successfully scanned item to green
            foreach (var productItem in OrderListDtos)
            {
                if (productItem.Code == itemBarcodeValue && productItem.StatusColor.ToLower() == statusColor.ToLower())
                {
                    OrderListDtos.ElementAt(index).IsBusy = true;
                    //This item has been scanned already
                    //Notify the user and return
                    Shell.Current.DisplayAlert("No duplicate scan allowed", "This item has been scanned already", "OK");
                    OrderListDtos.ElementAt(index).IsBusy = false;
                    itemCodeIsFound = productItem.Code == itemBarcodeValue ? true:false ;
                    return;
                }
                else if (productItem.Code == itemBarcodeValue)
                {
                    OrderListDtos.ElementAt(index).StatusColor = statusColor;
                    OrderListDtos.ElementAt(index++).IsBusy = true;

                    itemCodeIsFound = true;

                    break;
                }
                index++;
            }

            //No match was found the scanned product
            if (!itemCodeIsFound) 
            {
                Shell.Current.DisplayAlert("No match found", "There is no corresponding code found for this item. Please check if the CLIENT has ordered this ITEM and scan again!!!", "Cancel Scan");
                return;
            }

            //2.2 Update the percentage for boxview for each successfully scanned item
            float quantity = SelectedOrder.QuantityScanned + 1;
            float totalItems = SelectedOrder.ItemsCount;
            float percentage = (quantity / totalItems);

            int boxviewPercentageLength = (int)(percentage * 300);

            if (quantity == SelectedOrder.ItemsCount)
            {
                boxviewPercentageLength = 298;
            }

            SelectedOrder.QuantityScanned = quantity;
            SelectedOrder.QuantityScannedPercentage = boxviewPercentageLength; //280 is the total length of the boxview

            if (index > 0)
            {
                OrderListDtos.ElementAt(index - 1).IsBusy = false;
            }
            else
            {
                OrderListDtos.ElementAt(index).IsBusy = false;
            }
        }
    }
}
