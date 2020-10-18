using System;
using System.Collections.Generic;

namespace hw1_pv178
{
    public class IPlayer
    {
        public string Name { get; set; }
        public int PaperDamage { get; set; }
        public int RockDamage { get; set; }
        public int ScissorsDamage { get; set; }
        public int Hitpoints { get; set; }

        public List<int> Damages { get; set; }
    }
}
