using System.Windows;
using WpfApp1.ViewModels;
using LibraryLogicLayer;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Connect to logic layer only
            ILibraryDataService dataService = new LibraryDataService(); 
            DataContext = new MainViewModel(dataService);
        }
    }
}
