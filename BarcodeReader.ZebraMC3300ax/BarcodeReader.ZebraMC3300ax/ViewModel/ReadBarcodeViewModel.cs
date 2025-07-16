using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BarcodeReader.ZebraMC3300ax.ViewModel
{
    public class ReadBarcodeViewModel : INotifyPropertyChanged
    {
        public class Product
        {
            public string Barcode { get; set; }
            public string Item { get; set; }
        }

        public ReadBarcodeViewModel()
        {
            Itens = new ObservableCollection<Product>();
            LoadEvents();
        }

        private void LoadEvents()
        {
            MessagingCenter.Subscribe<object, string>(this, "BarcodeScanned", (sender, barcode) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (!Itens.Any(x => x.Barcode == barcode))
                    {
                        var item = (Itens.Count + 1).ToString();
                        Itens.Add(new Product
                        {
                            Barcode = barcode,
                            Item = item
                        });
                    }

                    Total = Itens.Count;
                });
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string nomePropriedade = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        private ObservableCollection<Product> _itens;
        public ObservableCollection<Product> Itens
        {
            get
            {
                return _itens;
            }
            set
            {
                _itens = value;
                OnPropertyChanged();
            }
        }

        private int _total;

        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

    }
}
