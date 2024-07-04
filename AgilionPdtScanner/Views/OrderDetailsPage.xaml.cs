
using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;
using AgilionPdtScanner.DTOs;
using AgilionPdtScanner.ViewModels;
using ZXing.QrCode.Internal;

namespace AgilionPdtScanner.Views;

[QueryProperty(nameof(SelectedOrder), "SelectedOrder")]
public partial class OrderDetailsPage : ContentPage
{
    private readonly OrderDetailsPageViewModel _viewModel;

    public OrderListDto SelectedOrder
    {
        get => _viewModel.SelectedOrder;
        set => _viewModel.SelectedOrder = value;
    }

    public OrderDetailsPage()
    {
        InitializeComponent();

        _viewModel = App.Services.GetService<OrderDetailsPageViewModel>();
        BindingContext = _viewModel;

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true,
        };

        
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is OrderDetailsPageViewModel viewModel)
        {
            await viewModel.ExecuteLoadOrderListCommand(viewModel.SelectedOrder);
        }
    }


    private void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        int secondsToVibrate = Random.Shared.Next(1, 3);
        TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

        Dispatcher.Dispatch(() =>
        {
            
            //DisplayAlert("Barcode", $"Barcodes: {e.Results[0].Format} -> {e.Results[0].Value}", "Ok");
            foreach (var itemScannedResult in e.Results) 
            {
                _viewModel.ExecuteItemScannedCommand(itemScannedResult.Value);
            }
            //Vibrate phone after successfully scanning
            Vibration.Default.Vibrate(vibrationLength);
        });
    }
    
    private async void OnTorch_Button_Clicked(object sender, EventArgs e)
    {
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
    }

    private void FlipCamera_Button_Clicked(object sender, EventArgs e)
    {
        cameraBarcodeReaderView.CameraLocation = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }
}