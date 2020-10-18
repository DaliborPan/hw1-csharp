using System;
using System.Collections.Generic;

namespace hw1_pv178
{
    public class Hero : IPlayer
    {
        public int Lvl { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public int MaxHp { get; set; } = 10;

        private const int START_DAMAGE = 2;
        private const int START_HP = 10;

        public int[] GetExperienceByWonFight { get; set; } = { 10, 5, 5, 5, 4, 4, 4, 4, 5 };
        public int[] ExperienceNeeded { get; set; } = { 10, 20, 30, 40, 50, 60, 70, 80, 100 };

        public Hero()
        {
            RockDamage = START_DAMAGE;
            ScissorsDamage = START_DAMAGE;
            PaperDamage = START_DAMAGE;
            Hitpoints = START_HP;

            Damages = new List<int>() { PaperDamage, RockDamage, ScissorsDamage };
        }

        public int Attack(string weapon)
        {
            switch (weapon)
            {
                case "paper":
                    return 0;
                case "rock":
                    return 1;
                case "scissors":
                    return 2;
                default:
                    return -1; // Unknown attack
            }
        }

        public void Up(string weapon, int val)
        {
            switch (weapon)
            {
                case "paper":
                    PaperDamage += val;
                    Damages[0] = PaperDamage;
                    return;
                case "rock":
                    RockDamage += val;
                    Damages[1] = RockDamage;
                    return;
                case "scissors":
                    ScissorsDamage += val;
                    Damages[2] = ScissorsDamage;
                    return;
                default:
                    return; // Unknown attack
            }
        }
    }

}
