using System.Windows;
using System.Windows.Controls; // Для Page


namespace WpfApp2.Views
{
    public partial class MainWindow : Window
    {
        private readonly Page _personsPage;
        private readonly Page _agreementsPage;

        public MainWindow()
        {
            InitializeComponent();

            _personsPage = new PersonsPage();
            _agreementsPage = new AgreementsPage();

            // Загружаем начальную страницу
            MainFrame.Navigate(_personsPage);
        }

        private void NavigateToPersonsPage_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content != _personsPage)
            {
                MainFrame.Navigate(_personsPage);
            }
        }

        private void NavigateToAgreementsPage_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content != _agreementsPage)
            {
                MainFrame.Navigate(_agreementsPage);
            }
        }
    }
}