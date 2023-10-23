using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
    public class ListeNGOTable : ObservableCollection<NGOTable>
    {

        [XmlAttribute("Collection")]
        public ObservableCollection<NGOTable> Collection => this;
    }
}
