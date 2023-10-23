using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GPAOnGo.NGO;

namespace GPAOnGo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CustomPage : ContentPage
    {

        public CustomPage():base()
        {
        }

        public static readonly BindableProperty FormattedTitleProperty = BindableProperty.Create(nameof(FormattedTitle), typeof(FormattedString), typeof(CustomPage), null);
        public FormattedString FormattedTitle
        {
            get { return (FormattedString)GetValue(FormattedTitleProperty); }
            set
            {
                SetValue(FormattedTitleProperty, value);
            }
        }

        public static readonly BindableProperty FormattedSubtitleProperty = BindableProperty.Create(nameof(FormattedSubtitle), typeof(FormattedString), typeof(CustomPage), null);
        public FormattedString FormattedSubtitle
        {
            get
            {
                return (FormattedString)GetValue(FormattedSubtitleProperty);
            }
            set
            {
                SetValue(FormattedSubtitleProperty, value);
            }
        }

        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(nameof(Subtitle), typeof(string), typeof(CustomPage), null);
        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set
            {
                SetValue(SubtitleProperty, value);
            }
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App._utilisateur = null;
            App._utilisateur = new Utilisateur();
            await Navigation.PopToRootAsync();
        }
    }
}