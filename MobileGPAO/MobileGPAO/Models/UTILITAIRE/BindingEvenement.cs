using System.ComponentModel;
using System.Xml.Serialization;

namespace GPAOnGo.UTILITAIRE
{

    [XmlRoot("BindingEvenement")]
    public class BindingEvenement : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

   
        public BindingEvenement(string pMessage)
        {
            A_Message = pMessage;
        }

        string A_Message { get; set; }

        // Accès aux variables

        [XmlElement("Message")]
        public string Message
        {
            get { return A_Message; }
            set { A_Message = value; OnPropertyChanged("Message");}
        }
    }
}
