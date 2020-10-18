using System;
namespace hw1_pv178
{
    public class Game
    {
        public Hero Hero { get; set; }
        public GameStatus GameStatus { get; set; } = GameStatus.Chilling;
        public Enemy ActualEnemy { get; set; }

        public Game()
        {
            Console.WriteLine("Welcome in the game. Type 'new game' to start");
            string s = Console.ReadLine();

            if (s != "new game")
            {
                Console.WriteLine("WTF?");
                return;
            }
            Console.WriteLine("Type your name!");
            string name = Console.ReadLine();
            Hero = new Hero() { Name = name };
            ActualEnemy = new Enemy(1) { Name = "StartMonster" };
            Console.WriteLine();
            Console.WriteLine("--------------");
            Console.WriteLine(" Game started ");
            Console.WriteLine("--------------");
        }

        public void Program()
        {
            while (true)
            {
                if (Hero.Lvl == 10)
                {
                    Console.WriteLine("You have won the game. Congrats!");
                    Console.WriteLine("End of the game. See you next time!");
                    return;
                }
                Console.WriteLine("To see all comands, type <help>");
                Console.WriteLine("Type your command");
                string command = Console.ReadLine();

                switch (command)
                {
                    case "help":
                        Help();
                        break;
                    case "healer":
                        Healer();
                        break;
                    case "lvlup paper":
                        Lvl_up("paper");
                        break;
                    case "lvlup rock":
                        Lvl_up("rock");
                        break;
                    case "lvlup scissors":
                        Lvl_up("scissors");
                        break;
                    case "fight":
                        GameStatus = GameStatus.Fight;
                        if (Fighting() == -1)
                        {
                            Console.WriteLine("You have died.");
                            return;
                        }
                        ActualEnemy = new Enemy(Hero.Lvl);
                        Hero.Exp += Hero.GetExperienceByWonFight[Hero.Lvl - 1];
                        GameStatus = GameStatus.Chilling;
                        break;
                    case "hero":
                        PrintHero();
                        break;
                    case "enemy":
                        PrintEnemy();
                        break;
                }
            }
        }

        public void Commands()
        {
            Console.WriteLine("When fighting:");
            Console.WriteLine("     paper");
            Console.WriteLine("     rock");
            Console.WriteLine("     scissors");
            Console.WriteLine();
            Console.WriteLine("When NOT fighting");
            Console.WriteLine("     lvlup paper");
            Console.WriteLine("     lvlup rock");
            Console.WriteLine("     lvlup scissors");
            Console.WriteLine("     healer");
            Console.WriteLine("     fight");
            Console.WriteLine("     hero");
            Console.WriteLine("     enemy");
            Console.WriteLine();
        }

        public void AboutGame()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("     RPS      ");
            Console.WriteLine("--------------");
            Console.WriteLine("<>   Aim of the game is to fight monsters, beat them and gain experiences.");
            Console.WriteLine("<>   With enough experience you can lvl up with command lvlup<weapon>, where weapon is paper, rock or scissors.");
            Console.WriteLine("<>   You can start fight with command <fight>. Then you select: paper, rock or scissors.");
            Console.WriteLine("     You will immediately see if you beat the enemy. Also you can see hitpoints of both player.");
            Console.WriteLine("     If your hitpoints get to zero, you have died end the game ended.");
            Console.WriteLine("<>   When you're not in fight, you can use command <healer> to heal yourself. Try <hero> to see your stats or <enemy> to see enemy's stats.");
            Console.WriteLine("<>   When you get to lvl 10 you win the game.");

            Console.WriteLine("<>   You start with 0 experiences and lvl 1.");
            Console.WriteLine("<>   Experiences needed to get new lvl.");
            Console.WriteLine("     Lvl / Exp");
            Console.WriteLine("      2  /  10");
            Console.WriteLine("      3  /  20");
            Console.WriteLine("      4  /  30");
            Console.WriteLine("      5  /  40");
            Console.WriteLine("      6  /  50");
            Console.WriteLine("      7  /  60");
            Console.WriteLine("      8  /  70");
            Console.WriteLine("      9  /  80");
            Console.WriteLine("      10 / 100");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
            Console.WriteLine("Good luck and have fun :-)");
            Console.WriteLine("--------------");
            Console.WriteLine("--------------");
        }

        public void Help()
        {
            Console.WriteLine("1 - See all commands you can use");
            Console.WriteLine("2 - About game");
            Console.WriteLine();
            Console.WriteLine("/* Choose 1 or 2 */");

            int option = 0;
            try
            {
                option = Int32.Parse(Console.ReadLine());
            } catch
            {
                Console.WriteLine("Bad option. Type number next time!");
                Help();
            }

            switch(option)
            {
                case 1:
                    Commands();
                    break;
                case 2:
                    AboutGame();
                    break;
                default:
                    Console.WriteLine("Bad option. 1 or 2 next time...");
                    Help();
                    break;
            }
        }

        public int Fight(int H_weapon)
        {
            if (GameStatus != GameStatus.Fight)
            {
                Console.WriteLine("bad status");
                return -1;
            }
            Random random = new Random();

            int E_weapon = random.Next(3);
            if (H_weapon == E_weapon)
            {
                return 0;
            }
            if ((H_weapon == 0 && E_weapon == 2) ||
                (H_weapon == 1 && E_weapon == 0) ||
                (H_weapon == 2 && E_weapon == 1))
            {
                // Lost round -> Hero's hp down by Enemy's weapon
                Hero.Hitpoints -= ActualEnemy.Damages[E_weapon];
                return -1;
            }
            else
            {
                // Won round -> Enemy's hp down by Hero's weapon
                ActualEnemy.Hitpoints -= Hero.Damages[H_weapon];
                return 1;
            }
        }

        public int Fighting()
        {
            while (Hero.Hitpoints > 0 && ActualEnemy.Hitpoints > 0)
            {
                Console.WriteLine("/* Choose your weapon! */");
                string attack = Console.ReadLine();
                if (attack == "win")
                {
                    return 0;
                }
                int heroAttack = Hero.Attack(attack);
                if (heroAttack == -1)
                {
                    Console.WriteLine("Unknown attack. Your turn again.");
                    continue; // Invalid attack
                }
                Console.WriteLine();

                switch (Fight(heroAttack))
                {
                    case -1:
                        Console.WriteLine("Monster won!");
                        PrintFightStatus();
                        break;
                    case 0:
                        Console.WriteLine("It's draw!");
                        PrintFightStatus();
                        break;
                    case 1:
                        Console.WriteLine("You won!");
                        PrintFightStatus();
                        break;
                }
                Console.WriteLine();
            }

            if (Hero.Hitpoints < 1)
            {
                return -1;
            }
            return 0;
        }

        public int Lvl_up(string weapon)
        {
            Console.WriteLine();
            if (Hero.Exp < Hero.ExperienceNeeded[Hero.Lvl - 1] || GameStatus == GameStatus.Fight)
            {
                Console.WriteLine("Not enough experience to get new Lvl. You need to fight more enemies");
                Console.WriteLine();
                return -1;
            }

            int add_damage;
            if (Hero.Lvl > 5)
            {
                Hero.MaxHp += 2;
                add_damage = 4;
            }
            else
            {
                Hero.MaxHp += 4;
                add_damage = 2;
            }

            switch (weapon)
            {
                case "paper":
                    Hero.Up("paper", add_damage);
                    break;
                case "rock":
                    Hero.Up("rock", add_damage);
                    break;
                case "scissors":
                    Hero.Up("scissors", add_damage);
                    break;
                default:
                    Console.WriteLine("Unknown weapon. Your lvl: " + Hero.Lvl);
                    return -1; // unknown weapon
            }
            Hero.Lvl++;
            ActualEnemy = new Enemy(Hero.Lvl);
            Console.WriteLine("Successfully leveled up! Your lvl: " + Hero.Lvl);
            Console.WriteLine();
            return 0;
        }

        public int Healer()
        {
            if (GameStatus == GameStatus.Chilling)
            {
                Hero.Hitpoints = Hero.MaxHp;
                Console.WriteLine("Hero's hitpoints: " + Hero.MaxHp);
                Console.WriteLine();
                return 0;
            }
            Console.WriteLine("You can't heal during fight."); // never happens tbh
            return -1;
        }

        private void PrintFightStatus()
        {
            Console.WriteLine("Your hitpoints: " + Hero.Hitpoints);
            Console.WriteLine("Monster's hitpoints: " + ActualEnemy.Hitpoints);
            Console.WriteLine();
        }

        public int PrintHero()
        {
            Console.WriteLine();
            Console.WriteLine("Your name: " + Hero.Name);
            Console.WriteLine("Paper damage:    " + Hero.PaperDamage);
            Console.WriteLine("Rock damage:     " + Hero.RockDamage);
            Console.WriteLine("Scissors damage: " + Hero.ScissorsDamage);
            Console.WriteLine("EXP:  " + Hero.Exp);
            Console.WriteLine("LVL:  " + Hero.Lvl);
            Console.WriteLine("HP:   " + Hero.Hitpoints);
            Console.WriteLine();
            return 0;
        }

        public int PrintEnemy()
        {
            Console.WriteLine();
            Console.WriteLine(ActualEnemy.Name);
            Console.WriteLine("Paper damage:    " + ActualEnemy.PaperDamage);
            Console.WriteLine("Rock damage:     " + ActualEnemy.RockDamage);
            Console.WriteLine("Scissors damage: " + ActualEnemy.ScissorsDamage);
            Console.WriteLine("HP:   " + ActualEnemy.Hitpoints);
            Console.WriteLine();
            return 0;
        }
        
    }
}
