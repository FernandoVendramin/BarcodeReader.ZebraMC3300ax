# 📦 Leitura de Códigos de Barras com DataWedge (Zebra) via BroadcastReceiver

Este guia explica como configurar o **DataWedge** nos dispositivos **Zebra (ex: MC3330XR)** e integrá-lo com um aplicativo Xamarin.Forms para receber leituras de código de barras via **BroadcastReceiver** no Android.

---

## ✅ Pré-requisitos

- Dispositivo Zebra com **DataWedge** instalado (ex: MC3330xR)
- Projeto Xamarin.Forms com o módulo Android acessível
- Permissão para usar `BroadcastReceiver`
- API mínima: Android 5.0 (API 21)

---

## 🛠️ Passo 1: Criar o Perfil no DataWedge

1. Abra o **DataWedge** no dispositivo Zebra.
2. Toque em **"Add new profile"** e dê um nome ao perfil, por exemplo: `ZebraReaderProfile`.
3. No perfil:
   - Ative o perfil: `ON`
   - Em **Associated apps**, associe com o pacote do seu app (ex: `com.companyname.barcodereader`).
   - Ative **Barcode Input**.
   - Vá em **Output** e ative **Intent Output**.
4. Configuração **Intent Output**
   - Ative: ✅ **Enable Intent output**
   - Intent action: `com.zebra.myapp.ACTION`
   - Intent delivery: **Broadcast Intent**
5. Em Associated apps, associe seu pacote Android (ex: `com.companyname.barcodereader`).

Configurações extras:

Para habilitar/desabilitar a leitura contínua, basta realizar as configurações abaixo:

1. Abra o **DataWedge** no dispositivo Zebra.
2. Acesse o Perfil criado, por exemplo: `ZebraReaderProfile`.
3. Na sessão **Barcode Input** acesse:
   - Scanner Configuration **>** Reader Parameters **>** Aim Type:
      - Use `Continuous Read` para leitura contínua;
      - Use `Trigger` para leitura individual;

---

## 📥 Passo 2: Criar o BroadcastReceiver no projeto Android

Crie uma classe para receber os dados do código de barras:

```csharp
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
```

---
## 🧩 3. Registrar o receiver no `MainActivity.cs`

No seu `MainActivity`, registre o receiver:

```csharp
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        BarcodeBroadcastReceiver _receiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            _receiver = new BarcodeBroadcastReceiver();
            RegisterReceiver(_receiver, new IntentFilter("com.zebra.myapp.ACTION"));
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterReceiver(_receiver);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
```

---
## 📱 4. Receber no Xamarin.Forms

Para receber o valor lido no dispositivo Zebra no projeto compartilhado, basta ler o evento através do `MessagingCenter`:

```csharp
MessagingCenter.Subscribe<object, string>(this, "BarcodeScanned", (sender, barcode) =>
{
    Device.BeginInvokeOnMainThread(() =>
    {
        // Valor recebido através do parâmetro barcode
    });
});
```

---

## 🧰 Recursos úteis
- [Documentação oficial do DataWedge (Zebra)](https://techdocs.zebra.com/datawedge/latest/)
- [Zebra DataWedge Intent API](https://techdocs.zebra.com/datawedge/latest/guide/api/)
