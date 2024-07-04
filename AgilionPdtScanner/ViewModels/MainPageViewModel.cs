using AgilionPdtScanner.DTOs;
using AgilionPdtScanner.Repositories;
using AgilionPdtScanner.Views;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AgilionPdtScanner.ViewModels
{
    internal class MainPageViewModel : BaseViewModel
    {
        private string _number;
        private string _name;
        private string _date;

        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        
        public IAsyncRelayCommand LoadOrderListCommand { get; }
        //public IAsyncRelayCommand OrderItemClickedCommand { get; }
        public IRelayCommand<OrderListDto> OrderItemClickedCommand { get; }
        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public ObservableCollection<OrderListDto>? OrderListDtos { get; set; }
        private readonly IReadRepository _db;
        public MainPageViewModel()
        {
            IsBusy = true;
            OrderListDtos = new ObservableCollection<OrderListDto>();
            LoadOrderListCommand = new AsyncRelayCommand(ExecuteLoadOrderListCommand);
        }
        public MainPageViewModel(IReadRepository db)
        {
            IsBusy = true;
            _db = db;
            OrderListDtos = new ObservableCollection<OrderListDto>();
            LoadOrderListCommand = new AsyncRelayCommand(ExecuteLoadOrderListCommand);

            OrderItemClickedCommand = new RelayCommand<OrderListDto>(OnOrderItemClicked);
        }
        private async Task ExecuteLoadOrderListCommand()
        {
            int count = 1;
                    
            try
            {
                List<OrderListDto> orders = await _db.GetShipmentOrderList(); 

                if (OrderListDtos != null)
                {
                    OrderListDtos.Clear();
                }

                foreach (var order in orders)
                {
                    var orderListDto = new OrderListDto()
                    {
                        Number = order.Number == null ? string.Empty : order.Number,
                        Name = order.Name == null ? string.Empty : order.Name,
                        Date = order.Date,
                        ItemsCount = order.ItemsCount,
                        Phonenumber = order.Phonenumber,
                        Numbering = count++
                    };
                    OrderListDtos.Add(orderListDto);
                }

            }
            catch (Exception ex)
            {
                //await _messageService.ShowAsync("Error", ex.Message); // Handle exceptions
            }
            IsBusy = false;
        }

        private async void OnOrderItemClicked(OrderListDto selectedOrder)
        {
            int count = 0;
            if (selectedOrder == null)
                return;
            //Identify the item being selected and add ActivityIndicator respectively
            foreach(var o in OrderListDtos)
            {
                if(o.Phonenumber == selectedOrder.Phonenumber)
                {
                    OrderListDtos.ElementAt(count).IsBusy = true;
                    break;
                }
                count++;
            }

            selectedOrder.Name = "Details: " + selectedOrder.Name;

            var navigationParameter = new Dictionary<string, object>
            {
                { "SelectedOrder", selectedOrder }
            };
            
            await Shell.Current.GoToAsync(nameof(OrderDetailsPage), navigationParameter);
            //await Shell.Current.GoToAsync($"{nameof(OrderDetailsPage)}?clientName={title}", navigationParameter);

            
        }

    }
}
