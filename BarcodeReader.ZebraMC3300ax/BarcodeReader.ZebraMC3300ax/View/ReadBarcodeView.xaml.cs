using BarcodeReader.ZebraMC3300ax.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarcodeReader.ZebraMC3300ax.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadBarcodeView : ContentPage
    {
        public ReadBarcodeView()
        {
            InitializeComponent();
            BindingContext = new ReadBarcodeViewModel();
        }
    }
}