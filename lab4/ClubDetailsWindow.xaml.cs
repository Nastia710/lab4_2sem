using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Lab_4.Classes;
using Lab_4.Enum;
using System.Text.RegularExpressions;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for ClubDetailsWindow.xaml
    /// </summary>
    public partial class ClubDetailsWindow : Window
    {
        private const string nameReg = @"^[А-ЯІЇЄҐ][а-яіїєґ']*(?:-[А-ЯІЇЄҐ][а-яіїєґ']*)?$";
        private const int minAge = 25;
        private const int maxAge = 95;
        private const int minLessons = 1;
        private const int maxLessons = 20;

        public Circle CurrentCircle { get; private set; }

        public ClubDetailsWindow()
        {
            InitializeComponent();
            this.Title = "Додати новий гурток";
            ManagerBirthDatePicker.SelectedDate = DateTime.Now;
            PopulateSectionComboBox();
        }

        public ClubDetailsWindow(Circle circleToEdit) : this()
        {
            this.Title = "Редагувати гурток";
            CurrentCircle = circleToEdit;
            LoadCircleData(circleToEdit);
        }

        private void PopulateSectionComboBox()
        {
            SectionComboBox.ItemsSource = System.Enum.GetValues(typeof(Sections));
        }

        private void LoadCircleData(Circle circle)
        {
            NameTextBox.Text = circle.Name;
            SectionComboBox.SelectedItem = circle.Section;
            FeeTextBox.Text = circle.Fee.ToString();
            LessonsPerMonthTextBox.Text = circle.LessonsPerMonth.ToString();
            StudentsCountTextBox.Text = circle.StudentsCount.ToString();

            if (circle.Manager != null)
            {
                ManagerNameTextBox.Text = circle.Manager.Name;
                ManagerSurnameTextBox.Text = circle.Manager.Surname;
                ManagerBirthDatePicker.SelectedDate = circle.Manager.BirthDate;
            }
            else
            {
                ManagerNameTextBox.Text = string.Empty;
                ManagerSurnameTextBox.Text = string.Empty;
                ManagerBirthDatePicker.SelectedDate = null;
            }
        }

        private bool ValidateName(string name, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show($"Будь ласка, введіть {fieldName}.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Regex.IsMatch(name, nameReg))
            {
                MessageBox.Show($"{fieldName} повинен починатися з великої літери, містити тільки українські літери, може містити один апостроф на слово та один дефіс між двома словами.", 
                    "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateBirthDate(DateTime birthDate)
        {
            if (birthDate > DateTime.Now)
            {
                MessageBox.Show("Дата народження не може бути в майбутньому.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Now.AddYears(-age)) age--;

            if (age < minAge)
            {
                MessageBox.Show($"Вік керівника повинен бути більшим за {minAge} років.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (age > maxAge)
            {
                MessageBox.Show($"Вік керівника не повинен бути більшим за {maxAge} років.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateLessonsPerMonth(int lessons)
        {
            if (lessons < minLessons || lessons > maxLessons)
            {
                MessageBox.Show($"Кількість занять на місяць може бути в межах від {minLessons} до {maxLessons}.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                SectionComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(FeeTextBox.Text) ||
                string.IsNullOrWhiteSpace(LessonsPerMonthTextBox.Text) ||
                string.IsNullOrWhiteSpace(StudentsCountTextBox.Text) ||
                string.IsNullOrWhiteSpace(ManagerNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ManagerSurnameTextBox.Text) ||
                ManagerBirthDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateName(ManagerNameTextBox.Text, "Ім'я керівника") ||
                !ValidateName(ManagerSurnameTextBox.Text, "Прізвище керівника"))
            {
                return;
            }

            if (!ValidateBirthDate(ManagerBirthDatePicker.SelectedDate.Value))
            {
                return;
            }

            if (!int.TryParse(FeeTextBox.Text, out int fee) || fee <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну вартість (ціле позитивне число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(LessonsPerMonthTextBox.Text, out int lessons))
            {
                MessageBox.Show("Будь ласка, введіть коректну кількість занять на місяць (ціле число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateLessonsPerMonth(lessons))
            {
                return;
            }

            if (!int.TryParse(StudentsCountTextBox.Text, out int students) || students <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну кількість учнів (ціле позитивне число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Manager manager = new Manager(
                ManagerNameTextBox.Text,
                ManagerSurnameTextBox.Text,
                ManagerBirthDatePicker.SelectedDate.Value
            );

            if (CurrentCircle == null)
            {
                CurrentCircle = new Circle(
                    NameTextBox.Text,
                    (Sections)SectionComboBox.SelectedItem,
                    manager,
                    fee,
                    lessons,
                    students
                );
            }
            else
            {
                CurrentCircle.Name = NameTextBox.Text;
                CurrentCircle.Section = (Sections)SectionComboBox.SelectedItem;
                CurrentCircle.Manager = manager;
                CurrentCircle.Fee = fee;
                CurrentCircle.LessonsPerMonth = lessons;
                CurrentCircle.StudentsCount = students;
            }

            DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}