using System;
using Xamarin.Forms;

namespace GPAOnGo.Behaviors
{
    public class EntryExtendedBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create<EntryExtendedBehavior, string>(p => p.Value, string.Empty, propertyChanged: OnValueChange, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty FormatProperty = BindableProperty.Create<EntryExtendedBehavior, string>(p => p.Format, string.Empty, propertyChanged: OnFormatChange);

        private static void OnValueChange(BindableObject bindable, string oldvalue, string newvalue)
        {
            var e = bindable as EntryExtendedBehavior;

            if (e == null)
                throw new Exception("ExtendedEntry.OnValueChange bindable == null");

            e.Value = newvalue;
        }

        private static void OnFormatChange(BindableObject bindable, string oldvalue, string newvalue)
        {
            var e = bindable as EntryExtendedBehavior;

            if (e == null)
                throw new Exception("ExtendedEntry.OnFormatChange bindable == null");

            e.Format = newvalue;
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.Focused += OnFocused;
            entry.Unfocused += OnUnfocused;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Focused -= OnFocused;
            entry.Unfocused -= OnUnfocused;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            Entry entry = sender as Entry;

            if (entry == null)
                throw new Exception("ExtendedEntry.OnFocus sender == null");

            entry.Text = entry.Text;
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            Entry entry = sender as Entry;

            if (entry == null)
                throw new Exception("ExtendedEntry.OnUnfocused sender == null");

            string unformattedValue = entry.Text;
      
            if(Format.Trim().Length - unformattedValue.Trim().Length > 0)
            {
                Value = Format.Substring(0, Format.Trim().Length - unformattedValue.Trim().Length) + unformattedValue.Trim();
            }
            entry.Text = unformattedValue;
        }
    }
}