using Lab_4.Classes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab_4.Enum;
using lab4;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private YouthCreativityCenter _center;

        public MainWindow()
        {
            InitializeComponent();

            _center = App.LoadYouthCenterData();
            ClubsListView.ItemsSource = _center.Circles;

            UpdateButtonVisibility();

            ClubsListView.SelectionChanged += ClubsListView_SelectionChanged;

            this.Closing += MainWindow_Closing;
        }

        private void ClubsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateButtonVisibility();
        }

        private void UpdateButtonVisibility()
        {
            bool isItemSelected = ClubsListView.SelectedItem != null;
            Add.Visibility = Visibility.Visible;
            
            Edit.Visibility = isItemSelected ? Visibility.Visible : Visibility.Collapsed;
            Delete.Visibility = isItemSelected ? Visibility.Visible : Visibility.Collapsed;
            View.Visibility = isItemSelected ? Visibility.Visible : Visibility.Collapsed;
        }

        /*private void InitializeData()
        {
            _center = new YouthCreativityCenter("Вул. Свободи, 10");

            Manager manager1 = new Manager("Іван", "Петренко", new DateTime(1980, 5, 15));
            Manager manager2 = new Manager("Олена", "Коваленко", new DateTime(1975, 11, 22));
            Manager manager3 = new Manager("Сергій", "Сидоров", new DateTime(1990, 3, 8));

            _center.AddClub(new Circle("Юний художник", Sections.Drawing, manager1, 300, 8, 15));
            _center.AddClub(new Circle("Сучасні танці", Sections.Dance, manager2, 450, 12, 25));
            _center.AddClub(new Circle("Авіамоделювання", Sections.Modeling, manager3, 250, 6, 10));
            _center.AddClub(new Circle("Вишивка", Sections.SoftToys, manager2, 200, 7, 20));
        }*/

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ClubDetailsWindow detailsWindow = new ClubDetailsWindow();
            if (detailsWindow.ShowDialog() == true)
            {
                if (detailsWindow.CurrentCircle != null)
                {
                    _center.AddClub(detailsWindow.CurrentCircle);
                    MessageBox.Show("Гурток успішно додано!", "Додавання гуртка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ClubsListView.SelectedItem is Circle selectedCircle)
            {
                ClubDetailsWindow detailsWindow = new ClubDetailsWindow(selectedCircle);
                if (detailsWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Гурток успішно відредаговано!", "Редагування гуртка", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClubsListView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть гурток для редагування.", "Редагувати гурток", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ClubsListView.SelectedItem is Circle selectedCircle)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Ви впевнені, що хочете видалити гурток '{selectedCircle.Name}'?",
                    "Видалити гурток",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    _center.RemoveClub(selectedCircle);
                    MessageBox.Show("Гурток успішно видалено!", "Видалення гуртка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть гурток для видалення.", "Видалити гурток", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (ClubsListView.SelectedItem is Circle selectedCircle)
            {
                ClubInfoViewWindow infoWindow = new ClubInfoViewWindow(selectedCircle);

                infoWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть гурток для перегляду інформації.", "Перегляд інформації", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.SaveYouthCenterData(_center);
        }
    }
}