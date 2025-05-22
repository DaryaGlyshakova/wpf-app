using System;
using System.Linq;
using System.Windows;
// using WpfApp2.AppData;

namespace WpfApp2.Views
{
    public partial class AddEditAgreementWindow : Window
    {
        private WPFEntities1 _context;
        private Agreements _currentAgreement;
        private bool _isEditMode;

        // Конструктор для ДОБАВЛЕНИЯ
        public AddEditAgreementWindow(WPFEntities1 context)
        {
            InitializeComponent();
            _context = context;
            _currentAgreement = new Agreements();
            _isEditMode = false;
            Title = "Добавление договора";
            LoadComboBoxesData();
            // Устанавливаем текущую дату открытия по умолчанию
            _currentAgreement.DataOpen = DateTime.Now;
            DataOpenDatePicker.SelectedDate = _currentAgreement.DataOpen;
        }

        // Конструктор для РЕДАКТИРОВАНИЯ
        public AddEditAgreementWindow(WPFEntities1 context, Agreements agreementToEdit)
        {
            InitializeComponent();
            _context = context;
            _currentAgreement = agreementToEdit;
            _isEditMode = true;
            Title = "Редактирование договора";
            LoadComboBoxesData();
            PopulateFields();
        }

        private void LoadComboBoxesData()
        {
            try
            {
                // Загружаем клиентов (можно добавить сортировку или фильтрацию, если список большой)
                PersonComboBox.ItemsSource = _context.Persons.OrderBy(p => p.Inn).ToList();
                TypeComboBox.ItemsSource = _context.Types.ToList();
                StatusAgreementComboBox.ItemsSource = _context.StatusAgreements.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки справочников: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void PopulateFields()
        {
            NumberTextBox.Text = _currentAgreement.Number;
            DataOpenDatePicker.SelectedDate = _currentAgreement.DataOpen;
            DataCloseDatePicker.SelectedDate = _currentAgreement.DataClose; // Может быть null
            NoteTextBox.Text = _currentAgreement.Note;

            PersonComboBox.SelectedValue = _currentAgreement.PersonID;
            TypeComboBox.SelectedValue = _currentAgreement.TypeID;
            StatusAgreementComboBox.SelectedValue = _currentAgreement.StatusID;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NumberTextBox.Text) ||
                PersonComboBox.SelectedItem == null ||
                TypeComboBox.SelectedItem == null ||
                StatusAgreementComboBox.SelectedItem == null ||
                DataOpenDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (Номер, Клиент, Тип, Статус, Дата открытия).", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Проверка на уникальность номера договора (если требуется на уровне приложения)
            string currentNumber = NumberTextBox.Text;
            int currentAgreementId = _currentAgreement.ID; // 0 для нового договора

            bool numberExists = _context.Agreements.Any(a => a.Number == currentNumber && a.ID != currentAgreementId);
            if (numberExists)
            {
                MessageBox.Show("Договор с таким номером уже существует.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            _currentAgreement.Number = NumberTextBox.Text;
            _currentAgreement.DataOpen = DataOpenDatePicker.SelectedDate.Value;
            _currentAgreement.DataClose = DataCloseDatePicker.SelectedDate; // Может быть null
            _currentAgreement.Note = NoteTextBox.Text;

            _currentAgreement.PersonID = (int)PersonComboBox.SelectedValue;
            _currentAgreement.TypeID = (int)TypeComboBox.SelectedValue;
            _currentAgreement.StatusID = (int)StatusAgreementComboBox.SelectedValue;

            try
            {
                if (!_isEditMode)
                {
                    _context.Agreements.Add(_currentAgreement);
                }

                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                var errorMessages = dbEx.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                MessageBox.Show($"Ошибки валидации: {fullErrorMessage}", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
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