using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;using GPAOnGo.NGO;
using System.Xml.Serialization;

using Xamarin.Forms;

namespace GPAOnGo.UTILITAIRE
{
    public class StyleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var someValue = (bool)value; 
            
            if (someValue)
                return (Style)App.Current.Resources["SmallBoldStartImportant_Label"]; 

            return (Style)App.Current.Resources["SmallBoldStart_Label"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class TypeToLabelVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NGOObjectItemIHM.enumItemIHMType oValue = (NGOObjectItemIHM.enumItemIHMType)value;
            switch(oValue)
            {
                case NGOObjectItemIHM.enumItemIHMType.Menu:
                    return false;
                default:
                    return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class TemplateSelector_PresentationGroupe : DataTemplateSelector
    {
        public DataTemplate Template_NA{ get; set; }
        public DataTemplate Template_ListeMultiple { get; set; }
        public DataTemplate Template_ListeSimple { get; set; }
        public DataTemplate Template_SfChart_ColumnSeries { get; set; }
        public DataTemplate Template_SfChart_LineSeries { get; set; }
        public DataTemplate Template_Menu { get; set; }
        public DataTemplate Template_TreeView { get; set; }
        public DataTemplate Template_ComboBox { get; set; }
        public DataTemplate Template_TextBox { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((NGOObject)item).Type)
            {
                case NGOObjectItemIHM.enumItemIHMType.Menu:
                    return Template_Menu;
                case NGOObjectItemIHM.enumItemIHMType.Detail:
                    return ((NGOObject)item).ListeChamps.Collection.Count == 1 ? Template_ListeSimple : Template_ListeMultiple;
                case NGOObjectItemIHM.enumItemIHMType.SfChart_ColumnSeries:
                    return Template_SfChart_ColumnSeries;
                case NGOObjectItemIHM.enumItemIHMType.SfChart_LineSeries:
                    return Template_SfChart_LineSeries;
                case NGOObjectItemIHM.enumItemIHMType.TreeView:
                    return Template_TreeView;
                default:
                    return Template_NA;
            }
        }
    }

    public class TemplateSelector_PresentationItems : DataTemplateSelector
    {
        public DataTemplate Template_Item_NA { get; set; }
        public DataTemplate Template_Item_Detail { get; set; }
        public DataTemplate Template_Item_Menu { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            switch (((NGOContenu)item).Type)
            {
                case NGOObjectItemIHM.enumItemIHMType.Menu:
                    return Template_Item_Menu;
                case NGOObjectItemIHM.enumItemIHMType.Detail:
                    return Template_Item_Detail;
                default:
                    return Template_Item_NA;
            }
        }
    }
}