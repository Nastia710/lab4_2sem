using System;
using System.Windows;
using Lab_4.Classes;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for ClubInfoViewWindow.xaml
    /// </summary>
    public partial class ClubInfoViewWindow : Window
    {
        public ClubInfoViewWindow(Circle circle)
        {
            InitializeComponent();
            this.DataContext = circle;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}