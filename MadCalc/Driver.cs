using System.ComponentModel;

namespace MadCalc
{
    public class Driver // todo: check drivers are less or eq to carrs count
    {
        [DisplayName("Надбавка за стаж")]
        public uint ExpirienceAdd { get; set; }
        [DisplayName("Надбавка за класс")]
        public uint ClassAdd { get; set; }
    }
}
