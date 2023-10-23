using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.ComponentModel;

namespace GPAOnGo.NGO
{
    public class NGOStatutListe : ObservableCollection<NGOStatut>, INotifyPropertyChanged
    {
  
        public NGOStatutListe()
        {
        }

        [XmlAttribute("Collection")]
        public ObservableCollection<NGOStatut> Collection => this;
    }
}
