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
        /*private const string nameReg = @"^[А-ЯІЇЄҐ][а-яіїєґ']*(?:-[А-ЯІЇЄҐ][а-яіїєґ']*)?$";
        private const int minAge = 25;
        private const int maxAge = 95;
        private const int minLessons = 1;
        private const int maxLessons = 20;*/

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
            /*NameTextBox.Text = circle.Name;
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
            }*/

            if (circle.Manager == null)
            {
                circle.Manager = new Manager("", "", DateTime.Now);
            }
        }

        /*private bool ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            if (!Validator.TryValidateObject(model, context, results, true))
            {
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return false;
            }
            return true;
        }*/

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