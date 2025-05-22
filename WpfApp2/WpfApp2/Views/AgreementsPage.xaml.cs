using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity; 



namespace WpfApp2.Views
{
    public partial class AgreementsPage : Page
    {
        private WPFEntities1 _context = new WPFEntities1();

        public AgreementsPage()
        {
            InitializeComponent();
            LoadAllAgreementsData(); // Загружаем все договоры при открытии страницы
        }

        private void LoadAllAgreementsData()
        {
            try
            {
                // Загружаем все договоры, включая связанные данные для отображения
                AgreementsGrid.ItemsSource = _context.Agreements
                                                 .Include(a => a.Persons) // Подгружаем связанного клиента
                                                 .Include(a => a.Types)   // Подгружаем связанный тип договора
                                                 .Include(a => a.StatusAgreements) // Подгружаем связанный статус договора
                                                 .ToList();
                SearchClientTextBox.Text = string.Empty; // Очищаем поле поиска
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных договоров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchAgreements_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchClientTextBox.Text.Trim().ToLower(); // Получаем текст и приводим к нижнему регистру для регистронезависимого поиска

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadAllAgreementsData(); // Если поле поиска пустое, показываем все
                return;
            }

            try
            {
                AgreementsGrid.ItemsSource = _context.Agreements
                                                 .Include(a => a.Persons)
                                                 .Include(a => a.Types)
                                                 .Include(a => a.StatusAgreements)
                                                 .Where(a => a.Persons.Inn.ToLower().Contains(searchText) || // Ищем по ИНН
                                                              a.Persons.Shifer.ToLower().Contains(searchText) || // Ищем по Шифру
                                                              (a.Persons.Type != null && a.Persons.Type.ToLower().Contains(searchText))) // Ищем по типу клиента (если есть и не null)
                                                 .ToList();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка поиска договоров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAllAgreements_Click(object sender, RoutedEventArgs e)
        {
            LoadAllAgreementsData();
        }

        private void AddAgreement_Click(object sender, RoutedEventArgs e)
        {
            var addEditWindow = new AddEditAgreementWindow(_context);
            bool? result = addEditWindow.ShowDialog();

            if (result == true)
            {
                LoadAllAgreementsData(); // Обновляем список договоров
            }
        }

        private void EditAgreement_Click(object sender, RoutedEventArgs e)
        {
            var selectedAgreement = AgreementsGrid.SelectedItem as Agreements;
            if (selectedAgreement == null)
            {
                MessageBox.Show("Пожалуйста, выберите договор для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var addEditWindow = new AddEditAgreementWindow(_context, selectedAgreement);
            bool? result = addEditWindow.ShowDialog();

            if (result == true)
            {
                LoadAllAgreementsData();
            }
            else
            {
                // Откатываем изменения, если пользователь нажал "Отмена"
                _context = new WPFEntities1();
                LoadAllAgreementsData();
            }
        }


        private void DeleteAgreement_Click(object sender, RoutedEventArgs e)
        {
            var selectedAgreement = AgreementsGrid.SelectedItem as Agreements;
            if (selectedAgreement == null)
            {
                MessageBox.Show("Пожалуйста, выберите договор для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить договор №{selectedAgreement.Number}?",
                                                       "Подтверждение удаления",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Agreements.Remove(selectedAgreement);
                    _context.SaveChanges();
                    LoadAllAgreementsData(); // Обновляем DataGrid
                    MessageBox.Show("Договор успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления договора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Пересоздаем контекст в случае ошибки, чтобы отменить изменения
                    _context = new WPFEntities1();
                    LoadAllAgreementsData();
                }
            }
        }
    }
}
        