using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace WinRT_XAML_Controls
{
    public sealed class AutoCompleteTextBox : Control
    {
        TextBox tb = null;
        ListBox lb = null;
        Grid g = null;
        
        public AutoCompleteTextBox()
        {
            this.DefaultStyleKey = typeof(AutoCompleteTextBox);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.tb = GetTemplateChild("tbChild") as TextBox;
            this.lb = GetTemplateChild("lbChild") as ListBox;

            this.g = GetTemplateChild("spContainer") as Grid;

            if (tb != null && this.lb != null)
            {
                tb.TextChanged += tb_TextChanged;
            }

            if (this.ItemsSource != null)
                this.lb.ItemsSource = ItemsSource;

            this.g.MaxHeight = this.MaxHeight;
        }

        public ICollection<string> ItemsSource
        {
            get { return (ICollection<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string Text { get { return (this.tb == null ? null : this.tb.Text); } }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ICollection<string>), typeof(AutoCompleteTextBox), new PropertyMetadata(null));

        void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lb.SelectionChanged -= lb_SelectionChanged;

            this.lb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (String.IsNullOrWhiteSpace(this.tb.Text) || this.ItemsSource == null || this.ItemsSource.Count == 0)
                return;

            var sel = (from d in this.ItemsSource where d.StartsWith(this.tb.Text) select d);

            if (sel != null && sel.Count() > 0)
            {
                this.lb.ItemsSource = sel;
                this.lb.Visibility = Windows.UI.Xaml.Visibility.Visible;

                this.lb.SelectionChanged += lb_SelectionChanged;
            }

            //tb.TextChanged += tb_TextChanged;
        }

        void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tb.TextChanged -= tb_TextChanged;

            this.tb.Text = (string)this.lb.SelectedValue;

            this.tb.TextChanged += tb_TextChanged;

            this.lb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
