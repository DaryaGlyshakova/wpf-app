using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp2.Views
{
    public partial class PersonsPage : Page
    {
        
        private WPFEntities1 _context = new WPFEntities1();

        public PersonsPage()
        {
            InitializeComponent();
            LoadPersonsData();
        }

        private void LoadPersonsData()
        {
            try
            {
                // Загружаем данные из таблицы Persons и передаем их в DataGrid
                // ToList() материализует запрос к БД
                PersonsGrid.ItemsSource = _context.Persons.ToList();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр окна добавления/редактирования в режиме добавления
            var addEditWindow = new AddEditPersonWindow(_context); // Передаем текущий контекст

            // Открываем окно как диалоговое и проверяем результат
            bool? result = addEditWindow.ShowDialog();

            // Если окно было закрыто с результатом true (т.е. данные были сохранены),
            // то обновляем DataGrid
            if (result == true)
            {
                LoadPersonsData(); // Перезагружаем данные, чтобы увидеть нового клиента
            }
        }

        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = PersonsGrid.SelectedItem as Persons;

            if (selectedPerson == null)
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создаем экземпляр окна добавления/редактирования в режиме редактирования,
            // передавая контекст и выбранного клиента
            var addEditWindow = new AddEditPersonWindow(_context, selectedPerson);

            bool? result = addEditWindow.ShowDialog();

            if (result == true)
            {
                // Обновляем DataGrid. Можно перезагрузить все,
                // или если известно, что только текущий элемент изменен, обновить только его.
                // Для простоты перезагрузим все.
                LoadPersonsData();
            }
            else
            {
                _context = new WPFEntities1(); // Пересоздаем контекст, чтобы отменить несохраненные изменения
                LoadPersonsData(); // И перезагружаем данные
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранного клиента
            var selectedPerson = PersonsGrid.SelectedItem as Persons;

            if (selectedPerson == null)
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Запрашиваем подтверждение удаления
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить клиента: {selectedPerson.Inn} ({selectedPerson.Shifer})?",
                                                       "Подтверждение удаления",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Удаляем клиента из контекста
                    _context.Persons.Remove(selectedPerson);
                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();
                    // Обновляем DataGrid
                    LoadPersonsData();
                    MessageBox.Show("Клиент успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
                {
                    // Обработка ошибок, связанных с ограничениями внешнего ключа
                    // (например, если у клиента есть связанные договоры)
                    MessageBox.Show($"Ошибка удаления: Невозможно удалить клиента, так как у него есть связанные данные (например, договоры).\nПодробнее: {dbEx.InnerException?.Message ?? dbEx.Message}", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
        
                    _context = new WPFEntities1(); // Пересоздаем контекст, чтобы отменить неудачные изменения
                    LoadPersonsData(); // Перезагружаем данные
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
         