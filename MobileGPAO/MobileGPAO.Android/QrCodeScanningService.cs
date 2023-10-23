using GPAOnGo.PISTOLET;
using Xamarin.Forms;
using ZXing.Mobile;

using System.Collections.Generic;


[assembly: Dependency(typeof(GPAOnGo.Droid.QrCodeScanningService))]

namespace GPAOnGo.Droid
{
    public class QrCodeScanningService : IQrCodeScanningService
    {

        public  async void ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions()
            {
                PossibleFormats = new List<ZXing.BarcodeFormat> {  ZXing.BarcodeFormat.CODE_39 }
            };

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scanner Via Camera",
                BottomText = ""
            };
            var scanResults = await scanner.Scan(optionsCustom);

            if (scanResults != null)
            {
                string pType = string.Empty;
                string pData = string.Empty;
                try
                {
                    pType = scanResults.ToString().Substring(0, scanResults.ToString().IndexOf("-"));
                    pData = scanResults.ToString().Substring(scanResults.ToString().IndexOf("-") + 1);
                }
                catch (System.Exception)
                {
                    pType = App.Type_CAB_Unknow;
                    pData = "";
                }
                switch (pType)
                {
                    case App.Type_CAB_Utilisateur:
                    case App.Type_CAB_Container:
                    case App.Type_CAB_OF:
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(pType, App.Subscribe_ScanCodeBarre_Tag, pData));
                        break;
                    default:
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Type_CAB_Unknow, App.Subscribe_ScanCodeBarre_Tag, ""));
                        break;
                }
            }
        }
    }
}