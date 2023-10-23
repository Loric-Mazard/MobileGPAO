using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using GPAOnGo.UTILITAIRE;

namespace GPAOnGo.IHM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCustomToolbarItem : ContentView
    {
        // Bindable Property definition for MenuText
        public static readonly BindableProperty MenuTextProperty = BindableProperty.Create(
                                propertyName: "MenuText",
                                returnType: typeof(string),
                                declaringType: typeof(ViewCustomToolbarItem),
                                defaultValue: "",
                                defaultBindingMode: BindingMode.OneWay,
                                propertyChanged: TextPropertyChanged);

        // This property name should be the same as the MenuTextProperty's propertyName field
        public string MenuText
        {
            get { return base.GetValue(MenuTextProperty).ToString(); }
            set { base.SetValue(MenuTextProperty, value); }
        }

        // This is the TextPropertyChanged event handler for the MenuTextProperty
        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewCustomToolbarItem)bindable;
            control.menuText.Text = newValue.ToString();
        }

        // Below is for IconImage, it's the same concept with MenuText
        // They all have 3 steps: BindableProperty, public property, PropertyChanged event handler
        public static readonly BindableProperty IconImageProperty = BindableProperty.Create(
                                propertyName: "IconImage",
                                returnType: typeof(string),
                                declaringType: typeof(ViewCustomToolbarItem),
                                defaultValue: "",
                                defaultBindingMode: BindingMode.OneWay,
                                propertyChanged: ImageSourcePropertyChanged);

        public string IconImage
        {
            get { return base.GetValue(IconImageProperty).ToString(); }
            set { base.SetValue(IconImageProperty, value); }
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewCustomToolbarItem)bindable;
            // Notice here we are updating the ImageSource of the control's image property
            control.iconImage.Source = ImageSource.FromFile(newValue.ToString());
        }

        // Below is the constructor of the ContentView, nothing special
        public ViewCustomToolbarItem()
        {
            InitializeComponent();
        }
    }
}