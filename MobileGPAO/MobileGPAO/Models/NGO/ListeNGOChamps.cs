using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    [XmlRoot("ListeNGOChamps")]
    public class ListeNGOChamps : ObservableCollection<NGOChamps>
    {
        [XmlAttribute("Collection")]
        public ObservableCollection<NGOChamps> Collection => this;
    }
}