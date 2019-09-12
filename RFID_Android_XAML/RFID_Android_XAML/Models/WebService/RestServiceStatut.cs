using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Linq;
using RFID_Android_XAML.WEBSERVICE;

namespace RFID_Android_XAML.WEBSERVICE
{
    public class RestServiceStatut
    {
        public RestServiceStatut(string content)
        {
            // Parse la réponse en XML
            XDocument doc = XDocument.Parse(content);
            foreach (var item in doc.Descendants("statut"))
            {
                this.valeur = int.Parse(item.Element("statut_val").Value);
            }

            // Vérifie si on doit afficher un msg
            foreach (var item in doc.Descendants("statut"))
            {
                this.avecDesccripton = int.Parse(item.Element("statut_avecdesc").Value);
                if (this.avecDesccripton == 1)
                {
                    this.description = item.Element("statut_desc").Value;
                }
            }
        }

        int valeur { get; set; }

        int avecDesccripton { get; set; }

        string description { get; set; }

        // Accès aux variables
        public int Valeur
        {
            get { return valeur; }
        }

        public int AvecDesccripton
        {
            get { return avecDesccripton; }
        }

        public string Description
        {
            get { return description; }
        }

        async public Task<int> traitementStatut()
        {
            var actionPage = App.Current.MainPage;
            int ReturntraitementStatut = valeur;

            
            switch(ReturntraitementStatut)
            {
                case -2:
                    //on ferme l'application
                    await actionPage.DisplayAlert("ERROR_FATAL", Description + Environment.NewLine + "Merci de contacter le service informatique CAWE", "Ok");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    break;
                case -1:
                    if (AvecDesccripton == 1)
                    {
                        await actionPage.DisplayAlert(null, Description, "OK");
                    }

                    if (actionPage.Navigation != null)
                        actionPage = actionPage.Navigation.NavigationStack.Last();

                    await actionPage.Navigation.PopAsync();
                    break;
                case 0:
                    if (AvecDesccripton == 1)
                    {
                        App._toastManager.LongMessage(Description);
                    }
                    break;
                case 1:
                    if (AvecDesccripton == 1)
                    {
                        App._toastManager.LongMessage(Description);
                    }
                    break;
                case 2:
                    if (AvecDesccripton == 1)
                    {
                        if (await actionPage.DisplayAlert(null, Description, "Oui", "Non"))
                        {
                            ReturntraitementStatut = 1;
                        }
                    }
                    break;
            }
           
            return ReturntraitementStatut;
        }
    }
}
