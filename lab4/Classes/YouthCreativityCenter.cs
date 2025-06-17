using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Lab_4.Classes
{
    public class YouthCreativityCenter : INotifyPropertyChanged
    {
        private string _address;
        private List<Circle> _circles;
        public YouthCreativityCenter(string address)
        {
            Address = address;
            Circles = new List<Circle>();
        }

        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public List<Circle> Circles
        {
            get => _circles;
            set
            {
                _circles = value; OnPropertyChanged(nameof(Circles));
            }
        }

        public void AddClub(Circle circle)
        {
            if (circle != null)
            {
                _circles.Add(circle);
            }
        }

        public void RemoveClub(Circle circle)
        {
            if (circle != null && _circles.Contains(circle))
            {
                _circles.Remove(circle);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Будинок дитячої творчості за адресою: {Address}");
            sb.AppendLine("Список гуртків:");
            foreach (var circle in Circles)
            {
                sb.AppendLine(circle.ToString());
            }
            return sb.ToString();
        }
        public string ShortString => ToShortString();

        public string ToShortString()
        {
            int totalStudents = 0;
            foreach (var circle in Circles)
            {
                totalStudents += circle.StudentsCount;
            }

            return $"Будинок дитячої творчості\n" +
                   $"Адреса: {Address}\n" +
                   $"Загальна кількість учнів: {totalStudents}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToString)));
        }
    }
}
