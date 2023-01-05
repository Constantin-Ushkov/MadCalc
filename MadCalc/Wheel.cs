using System;
using System.ComponentModel;

namespace MadCalc
{
    public class Wheel
    {
        [DisplayName("Колличество")]
        public uint Count { get; set; } // no more than car_count * 10
        [DisplayName("Стоимость (штука)")]
        public float Cost { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; } // no more than current date
    }
}
