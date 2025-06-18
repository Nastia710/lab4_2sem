using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Lab_4.Classes;
using Lab_4.Enum;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for ClubDetailsWindow.xaml
    /// </summary>
    public partial class ClubDetailsWindow : Window
    {
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

            if (!int.TryParse(FeeTextBox.Text, out int fee) || fee <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну вартість (ціле позитивне число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(LessonsPerMonthTextBox.Text, out int lessons) || lessons <= 0)
            {
                MessageBox.Show("Будь ласка, введіть коректну кількість занять на місяць (ціле позитивне число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
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