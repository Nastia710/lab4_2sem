using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using Lab_4.Classes;
using Lab_4.Enum;
using System.Collections.Generic;
using System.Linq;

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
            CurrentCircle = new Circle("", default(Sections), new Manager("", "", DateTime.Now), 0, 0, 0);
            this.DataContext = CurrentCircle;
            ManagerBirthDatePicker.SelectedDate = DateTime.Now;
            PopulateSectionComboBox();
        }

        public ClubDetailsWindow(Circle circleToEdit) : this()
        {
            this.Title = "Редагувати гурток";
            CurrentCircle = circleToEdit;
            this.DataContext = CurrentCircle;
            LoadCircleData(circleToEdit);
        }

        private void PopulateSectionComboBox()
        {
            SectionComboBox.ItemsSource = System.Enum.GetValues(typeof(Sections));
        }

        private void LoadCircleData(Circle circle)
        {
            if (circle.Manager == null)
            {
                circle.Manager = new Manager("", "", DateTime.Now);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var contextCircle = new ValidationContext(CurrentCircle, null, null);
            var resultsCircle = new List<ValidationResult>();
            bool isCircleValid = Validator.TryValidateObject(CurrentCircle, contextCircle, resultsCircle, true);

            var contextManager = new ValidationContext(CurrentCircle.Manager, null, null);
            var resultsManager = new List<ValidationResult>();
            bool isManagerValid = Validator.TryValidateObject(CurrentCircle.Manager, contextManager, resultsManager, true);


            if (!isCircleValid || !isManagerValid)
            {
                string errors = string.Join(Environment.NewLine, resultsCircle.Select(r => r.ErrorMessage));
                if (resultsManager.Any())
                {
                    errors += Environment.NewLine + string.Join(Environment.NewLine, resultsManager.Select(r => r.ErrorMessage));
                }

                MessageBox.Show("Будь ласка, виправте помилки вводу:" + Environment.NewLine + errors,
                                "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (!int.TryParse(FeeTextBox.Text, out int fee))
            {
                MessageBox.Show("Будь ласка, введіть коректну вартість (ціле позитивне число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(LessonsPerMonthTextBox.Text, out int lessons))
            {
                MessageBox.Show("Будь ласка, введіть коректну кількість занять на місяць (ціле число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(StudentsCountTextBox.Text, out int students))
            {
                MessageBox.Show("Будь ласка, введіть коректну кількість учнів (ціле позитивне число).", "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CurrentCircle.Manager == null)
            {
                CurrentCircle.Manager = new Manager(
                    ManagerNameTextBox.Text,
                    ManagerSurnameTextBox.Text,
                    ManagerBirthDatePicker.SelectedDate.Value
                );
            }
            else
            {
                CurrentCircle.Manager.Name = ManagerNameTextBox.Text;
                CurrentCircle.Manager.Surname = ManagerSurnameTextBox.Text;
                CurrentCircle.Manager.BirthDate = ManagerBirthDatePicker.SelectedDate.Value;
            }

            CurrentCircle.Name = NameTextBox.Text;
            CurrentCircle.Section = (Sections)SectionComboBox.SelectedItem;
            CurrentCircle.Fee = fee;
            CurrentCircle.LessonsPerMonth = lessons;
            CurrentCircle.StudentsCount = students;

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