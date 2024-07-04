using AgilionPdtScanner.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace AgilionPdtScanner
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
                        
            BindingContext = App.Services?.GetService<MainPageViewModel>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainPageViewModel viewModel)
            {
                viewModel.LoadOrderListCommand.Execute(null);
            }
        }
    }

}
