using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class ListeNGOContenu : ObservableCollection<NGOContenu>
    {
        [XmlAttribute("Collection")]
        public ObservableCollection<NGOContenu> Collection => this;
    }
}
