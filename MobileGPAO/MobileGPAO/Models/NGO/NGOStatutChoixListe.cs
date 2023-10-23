using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GPAOnGo.NGO
{
    public class NGOStatutChoixListe : ObservableCollection<NGOStatutChoix>, INotifyPropertyChanged
    {

        public NGOStatutChoixListe()
        {
        }

        [XmlAttribute("Collection")]
        public ObservableCollection<NGOStatutChoix> Collection => this;

        public bool ValeurEnable(string pValeur, ref NGOStatutChoix pERPStatutChoix)
        {

            foreach(NGOStatutChoix oERPStatutChoix in this)
            {
                if(oERPStatutChoix.Valeur == pValeur)
                {
                    pERPStatutChoix = oERPStatutChoix;
                    return oERPStatutChoix.Enable;
                }
            }

            return true;
        }

        [XmlAttribute("ERPStatutChoix_Min")]
        public NGOStatutChoix ERPStatutChoix_Min
        {
            get
            {
                foreach (NGOStatutChoix oERPStatutChoix in this)
                {
                    if (oERPStatutChoix.Valeur_Min)
                    {
                        return oERPStatutChoix;
                    }
                }

                return new NGOStatutChoix(string.Empty, string.Empty, 0, "0");
            }
            
        }

        [XmlAttribute("ERPStatutChoix_Max")]
        public NGOStatutChoix ERPStatutChoix_Max
        {
            get
            {
                foreach (NGOStatutChoix oERPStatutChoix in this)
                {
                    if (oERPStatutChoix.Valeur_Max)
                    {
                        return oERPStatutChoix;
                    }
                }

                return new NGOStatutChoix(string.Empty, string.Empty, 0, "100");
            }
           
        }
    }
}
