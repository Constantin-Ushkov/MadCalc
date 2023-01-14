using System;
using System.Collections.Generic;

namespace MadCalc
{
    internal class CargoTarif
    {
        public string Name { get; set; }
        public string Formula { get; set; }
        public Func<int, int, Dictionary<int, float[]>, float> Calculus { get; set; }
        public Dictionary<int, float[]> Tarifs { get; set; }

        public float CalcTarif(int distance, int @class)
        {
            if (Tarifs.TryGetValue(distance, out var entry))
            {
                return entry[@class];
            }

            return Calculus(distance, @class, Tarifs);
        }
    }
}
