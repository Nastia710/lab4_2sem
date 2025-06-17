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
using Lab_4.Classes;
using static System.Collections.Specialized.BitVector32;

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
            InitializeData();
            ClubsListView.ItemsSource = _center.Circles;
        }

        private void InitializeData()
        {
            _center = new YouthCreativityCenter("Вул. Свободи, 10");

            Manager manager1 = new Manager("Іван", "Петренко", new DateTime(1980, 5, 15));
            Manager manager2 = new Manager("Олена", "Коваленко", new DateTime(1975, 11, 22));
            Manager manager3 = new Manager("Сергій", "Сидоров", new DateTime(1990, 3, 8));

            _center.AddClub(new Circle("Юний художник", Sections.Drawing, manager1, 300, 8, 15));
            _center.AddClub(new Circle("Сучасні танці", Sections.Dance, manager2, 450, 12, 25));
            _center.AddClub(new Circle("Авіамоделювання", Sections.Modeling, manager3, 250, 6, 10));
            _center.AddClub(new Circle("Вишивка", Sections.SoftToys, manager2, 200, 7, 20));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}