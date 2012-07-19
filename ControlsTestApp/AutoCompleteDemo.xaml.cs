using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace ControlsTestApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AutoCompleteDemo : ControlsTestApp.Common.LayoutAwarePage
    {
        public AutoCompleteDemo()
        {
            this.InitializeComponent();
        }

        string prepopVal = "Autocomplete, or Word completion, is a feature provided by many web browsers, e-mail programs, search engine interfaces, source code editors, database query tools, word processors, and command line interpreters. Autocomplete is also available for, or already integrated in, general text editors. Autocomplete involves the program predicting a word or phrase that the user wants to type in without the user actually typing it in completely. This feature is effective when it is easy to predict the word being typed based on those already typed, such as when there are a limited number of possible or commonly used words (as is the case with e-mail programs, web browsers, or command line interpreters), or when editing text written in a highly-structured, easy-to-predict language (as in source code editors). It can also be very useful in text editors, when the prediction is based on a list of words in one or more languages. For special purposes, like medical or technical texts a word list of terms in that field is used. Many autocomplete programs are also learning new words as the user has written them a few times, and can suggest alternatives based on the habits of the individual user. Autocomplete, or word prediction, speeds up human-computer interactions in environments to which it is well suited.";
        char[] sepArr = { ' ', '.', ',' };

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<string> col = new List<string>();
            string[] parts = prepopVal.ToLower().Split(sepArr, StringSplitOptions.RemoveEmptyEntries);

            col.AddRange(parts.Distinct());

            this.actbTest.ItemsSource = col;

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void btnPrompt_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog md = new MessageDialog(this.actbTest.Text, "Test");
            await md.ShowAsync();
        }
    }
}