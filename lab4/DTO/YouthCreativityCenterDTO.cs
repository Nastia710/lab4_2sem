using Lab_4.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.DTOs
{
    public class YouthCreativityCenterDTO
    {
        public string Addres { get; set; }
        public ObservableCollection<CircleDTO> Circles { get; set; } = new ObservableCollection<CircleDTO>();
    }
}
