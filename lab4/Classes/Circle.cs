using Lab_4.Enum;
using Lab_4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_4.Classes
{
    public class Circle : INotifyPropertyChanged
    {
        private string _name;
        private Sections _section;
        private Manager _manager;
        private int _fee;
        private int _lessonsPerMonth;
        private int _studentsCount;

        public Circle(string name, Sections section, Manager manager, int fee, int lessonsPerMonth, int studentsCount)
        {
            Name = name;
            Section = section;
            Manager = manager;
            Fee = fee;
            LessonsPerMonth = lessonsPerMonth;
            StudentsCount = studentsCount;
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public Sections Section
        {
            get => _section;
            set { _section = value; OnPropertyChanged(nameof(Section)); }
        }

        public Manager Manager
        {
            get => _manager;
            set { _manager = value; OnPropertyChanged(nameof(Manager)); }
        }
        public int Fee
        {
            get => _fee;
            set { _fee = value; OnPropertyChanged(nameof(Fee)); }
        }
        public int LessonsPerMonth
        {
            get => _lessonsPerMonth;
            set { _lessonsPerMonth = value; OnPropertyChanged(nameof(LessonsPerMonth)); }
        }
        public int StudentsCount
        {
            get => _studentsCount;
            set { _studentsCount = value; OnPropertyChanged(nameof(StudentsCount)); }
        }
        public override string ToString()
        {
            return $"Назва гуртка: {Name} Секція: {Section} Керівник: {Manager} Оплата: {Fee} грн Занять на місяць: {LessonsPerMonth} Кількість учнів: {StudentsCount}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToString)));
        }
    }
}
