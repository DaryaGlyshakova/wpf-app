using System;
using System.Linq;
using System.Windows;
namespace WpfApp2.Views
{
    public partial class AddEditPersonWindow : Window
    {
        private WPFEntities1 _context;
        private Persons _currentPerson; // Сущность для добавления или редактирования
        private bool _isEditMode;     // Флаг режима (true - редактирование, false - добавление)

        // Конструктор для режима ДОБАВЛЕНИЯ нового клиента
        public AddEditPersonWindow(WPFEntities1 context)
        {
            InitializeComponent();
            _context = context;
            _currentPerson = new Persons(); // Создаем нового клиента
            _isEditMode = false;
            DataContext = _currentPerson; // Устанавливаем контекст данных для привязок (если бы они были в XAML)
                                         
            Title = "Добавление клиента";
            LoadComboBoxesData();
            // Устанавливаем текущую дату по умолчанию для нового клиента
            _currentPerson.Data = DateTime.Now;
            DataDatePicker.SelectedDate = _currentPerson.Data;
        }

        // Конструктор для режима РЕДАКТИРОВАНИЯ существующего клиента
        public AddEditPersonWindow(WPFEntities1 context, Persons personToEdit)
        {
            InitializeComponent();
            _context = context;
            _currentPerson = personToEdit; // Используем переданного клиента
            _isEditMode = true;
            DataContext = _currentPerson;
            Title = "Редактирование клиента";
            LoadComboBoxesData();
            PopulateFields(); // Заполняем поля данными редактируемого клиента
        }

        // Загрузка данных для выпадающих списков
        private void LoadComboBoxesData()
        {
            try
            {
                OrgLicenseComboBox.ItemsSource = _context.OrgLicenses.ToList();
                VerietyComboBox.ItemsSource = _context.Verieties.ToList();
                StatusComboBox.ItemsSource = _context.Statuses.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки справочников: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close(); // Закрываем окно, если справочники не загрузились
            }
        }

        // Заполнение полей данными, если это режим редактирования
        private void PopulateFields()
        {
            InnTextBox.Text = _currentPerson.Inn;
            TypeTextBox.Text = _currentPerson.Type;
            ShiferTextBox.Text = _currentPerson.Shifer;
            DataDatePicker.SelectedDate = _currentPerson.Data;

            if (_currentPerson.OrgLicenseID.HasValue)
                OrgLicenseComboBox.SelectedValue = _currentPerson.OrgLicenseID;

            VerietyComboBox.SelectedValue = _currentPerson.VerietyID;
            StatusComboBox.SelectedValue = _currentPerson.StatusID;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Простая валидация (можно расширить)
            if (string.IsNullOrWhiteSpace(InnTextBox.Text) ||
                string.IsNullOrWhiteSpace(TypeTextBox.Text) ||
                string.IsNullOrWhiteSpace(ShiferTextBox.Text) ||
                DataDatePicker.SelectedDate == null ||
                VerietyComboBox.SelectedItem == null ||
                StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Обновляем свойства _currentPerson из полей ввода
            _currentPerson.Inn = InnTextBox.Text;
            _currentPerson.Type = TypeTextBox.Text;
            _currentPerson.Shifer = ShiferTextBox.Text;
            _currentPerson.Data = DataDatePicker.SelectedDate.Value;

            // Получаем ID из ComboBox
            // OrgLicenseID может быть NULL, поэтому проверяем SelectedItem
            _currentPerson.OrgLicenseID = OrgLicenseComboBox.SelectedItem != null ? (int?)OrgLicenseComboBox.SelectedValue : null;
            _currentPerson.VerietyID = (int)VerietyComboBox.SelectedValue;
            _currentPerson.StatusID = (int)StatusComboBox.SelectedValue;

            try
            {
                if (!_isEditMode) // Если режим добавления
                {
                    _context.Persons.Add(_currentPerson);
                }
               

                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // Устанавливаем результат диалога для родительского окна
                this.Close();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                // Обработка ошибок валидации Entity Framework
                var errorMessages = dbEx.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(dbEx.Message, " Ошибки валидации: ", fullErrorMessage);
                MessageBox.Show(exceptionMessage, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}\nInnerException: {ex.InnerException?.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}