using Lab_4.Enum;
using Lab_4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab_4.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Lab_4.Classes
{
    public class Circle : BaseValidationViewModel
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

        [Required(ErrorMessage = "Назва гуртка є обов'язковою")]
        [RegularExpression(@"^[А-ЯІЇЄҐ][а-яіїєґ'\s]*(?:-[А-ЯІЇЄҐ][а-яіїєґ'\s]*)?$",
            ErrorMessage = "Назва гуртка повинна починатися з великої літери та містити тільки українські літери, пробіли, апострофи або дефіси.")]
        [Length(5,30, ErrorMessage = "Назва гуртка повинна мати від 5 до 30 символів")]
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
        [Range(1, int.MaxValue, ErrorMessage = "Вартість повинна бути більше 0")]
        public int Fee
        {
            get => _fee;
            set { _fee = value; OnPropertyChanged(nameof(Fee)); }
        }
        [Range(1, 20, ErrorMessage = "Кількість занять на місяць може бути в межах від 1 до 20")]
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
            return $"Назва гуртка: {Name}" + Environment.NewLine +
                   $"Секція: {Section}" + Environment.NewLine +
                   $"Керівник: {Manager}" + Environment.NewLine +
                   $"Оплата: {Fee} грн" + Environment.NewLine +
                   $"Занять на місяць: {LessonsPerMonth}" + Environment.NewLine +
                   $"Кількість учнів: {StudentsCount}";
        }

        public CircleDTO ToDTO()
        {
            return new CircleDTO
            {
                Name = this.Name,
                Section = this.Section,
                Manager = this.Manager?.ToDTO(),
                Fee = this.Fee,
                LessonsPerMonth = this.LessonsPerMonth,
                StudentsCount = this.StudentsCount
            };
        }

        public static Circle FromDTO(CircleDTO dto)
        {
            if (dto == null) return null;
            return new Circle(
                dto.Name,
                dto.Section,
                Manager.FromDTO(dto.Manager),
                dto.Fee,
                dto.LessonsPerMonth,
                dto.StudentsCount
            );
        }
    }
}
