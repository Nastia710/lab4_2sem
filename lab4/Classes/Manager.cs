using Lab_4.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Classes
{
    public class Manager : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private DateTime _birthDate;

        public Manager(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Surname
        {
            get => _surname;
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
        }
        public string ManagerFullName => ToString();
        public override string ToString()
        {
            return $"{Name} {Surname}, {BirthDate:dd.MM.yyyy}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToString)));
        }
        public ManagerDTO ToDTO()
        {
            return new ManagerDTO
            {
                Name = this.Name,
                Surname = this.Surname,
                BirthDate = this.BirthDate
            };
        }
        public static Manager FromDTO(ManagerDTO dto)
        {
            if (dto == null) return null;
            return new Manager(dto.Name, dto.Surname, dto.BirthDate);
        }
    }
}
