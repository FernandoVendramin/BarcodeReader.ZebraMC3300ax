using Android.App;

namespace ZebraTestForms.Droid
{
    using Android.Content;
    using Xamarin.Forms;

    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "com.zebra.myapp.ACTION" })]
    public class BarcodeBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.HasExtra("com.symbol.datawedge.data_string"))
            {
                string barcode = intent.GetStringExtra("com.symbol.datawedge.data_string");
                System.Diagnostics.Debug.WriteLine($"[BARCODE] Leitura: {barcode}");
                MessagingCenter.Send<object, string>(this, "BarcodeScanned", barcode);
            }
        }
    }
}