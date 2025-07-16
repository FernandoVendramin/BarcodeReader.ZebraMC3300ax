using BarcodeReader.ZebraMC3300ax.View;
using Xamarin.Forms;

namespace ZebraTestForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new ReadBarcodeView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
