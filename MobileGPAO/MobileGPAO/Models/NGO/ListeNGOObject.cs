using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class ListeNGOObject : ObservableCollection<NGOObject>
    {

        [XmlAttribute("Collection")]
        public ObservableCollection<NGOObject> Collection => this;
    }
}
