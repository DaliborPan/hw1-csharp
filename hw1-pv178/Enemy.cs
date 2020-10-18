using System;
using System.Collections.Generic;

namespace hw1_pv178
{
    public class Enemy : IPlayer
    {
        public int Lvl { get; set; }
        private const int START_HP = 10;

    public Enemy(int lvl)
        {
            RockDamage = lvl;
            ScissorsDamage = lvl;
            PaperDamage = lvl;
            Hitpoints = START_HP;

            for (int i = 2; i <= lvl; i += 2)
            {
                Hitpoints += 5;
            }
            if (lvl == 9)
            {
                Hitpoints = 50;
            }
            
            if (lvl > 5)
            {
                for (int i = 5; i < lvl; i++)
                {
                    Random random = new Random();
                    int r = random.Next(3);
                    switch(r)
                    {
                        case 0:
                            RockDamage += 2;
                            break;
                        case 1:
                            ScissorsDamage += 2;
                            break;
                        default:
                            PaperDamage += 2;
                            break;
                    }
                }
                if (lvl == 9)
                {
                    PaperDamage--; RockDamage--; ScissorsDamage--;
                }
            }
            
            Lvl = lvl;

            Damages = new List<int>() { PaperDamage, RockDamage, ScissorsDamage };
        }
    }
}
