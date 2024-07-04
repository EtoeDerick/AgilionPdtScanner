

using AgilionPdtScanner.Views;

namespace AgilionPdtScanner
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
                
            Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
        }       
    }
}
