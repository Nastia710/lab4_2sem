using Lab_4.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_4.Classes
{
    public class Manager : BaseValidationViewModel
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

        [Required(ErrorMessage = "Ім'я є обов'язковим")]
        [RegularExpression(@"^[А-ЯІЇЄҐ][а-яіїєґ']*(?:-[А-ЯІЇЄҐ][а-яіїєґ']*)?$", 
            ErrorMessage = "Ім'я повинно починатися з великої літери, містити тільки українські літери, може містити один апостроф на слово та один дефіс між двома словами")]
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        [Required(ErrorMessage = "Прізвище є обов'язковим")]
        [RegularExpression(@"^[А-ЯІЇЄҐ][а-яіїєґ']*(?:-[А-ЯІЇЄҐ][а-яіїєґ']*)?$", 
            ErrorMessage = "Прізвище повинно починатися з великої літери, містити тільки українські літери, може містити один апостроф на слово та один дефіс між двома словами")]
        public string Surname
        {
            get => _surname;
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }

        [Required(ErrorMessage = "Дата народження є обов'язковою")]
        [CustomValidation(typeof(Manager), nameof(ValidateBirthDate))]
        public DateTime BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); }
        }

        public static ValidationResult ValidateBirthDate(DateTime birthDate, ValidationContext context)
        {
            if (birthDate > DateTime.Now)
            {
                return new ValidationResult("Дата народження не може бути в майбутньому");
            }

            int age = DateTime.Now.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Now.AddYears(-age)) age--;

            if (age < 25)
            {
                return new ValidationResult("Вік керівника повинен бути більшим за 25 років");
            }
            if (age > 95)
            {
                return new ValidationResult("Вік керівника не повинен бути більшим за 95 років");
            }

            return ValidationResult.Success;
        }

        public string ManagerFullName => ToString();
        public override string ToString()
        {
            return $"{Name} {Surname}, {BirthDate:dd.MM.yyyy}";
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
