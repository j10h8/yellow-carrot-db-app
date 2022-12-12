using System.Windows;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        private string _signedInUserName;

        public RecipeWindow(string signedInUserName)
        {
            _signedInUserName = signedInUserName;

            InitializeComponent();
        }
    }
}
