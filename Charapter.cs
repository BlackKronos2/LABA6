using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA6
{
    public class Charapter
    {
        protected string name;
        protected int health;
        protected int max_health;
        protected int impact_strength;
        protected int recovery;
        protected bool alive;

        public Charapter()
        {
            this.name = "";
            this.alive = false;
            this.health = 0;
            this.max_health = 0;
            this.impact_strength = 0;
            this.recovery = 0;
        }
        public Charapter(string n, int max_h, int im_str, int rec)
        {
            name = n;
            max_health = max_h;
            health = max_h;
            impact_strength = im_str;
            recovery = rec;
            alive = true;
        }
        public string Name
        {
            set
            {
                if (value.Length < 5)
                {
                    Console.WriteLine("Имя слишком короткое\n");
                }
                else
                {
                    this.name = value;
                }
            }
            get { return name; }
        }
        public int Max_health
        {
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Значение максимального меньше или равно 0");
                }
                else
                {
                    this.max_health = value;
                    this.health = value;
                }
            }
            get { return max_health; }
        }
        public int Health
        {
            get { return health; }
        }
        public int Impact_strength
        {
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Значение силы удара обычного персонажа меньше или равно 0");
                }
                else
                {
                    this.impact_strength = value;
                }
            }
            get { return impact_strength; }
        }
        public int Recovery
        {
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Значение пассивного исцеления персонажа-целителя меньше или равно 0");
                }
                else
                {
                    this.recovery = value;
                }
            }
            get { return recovery; }
        }
        public bool Alive
        {
            set { this.alive = value; }
            get { return alive; }
        }

        public void Damage(int a)
        {
            if (health - a <= 0)
                health = 0;
            else
                health -= a;
        }
        public void Treatment(int a)
        {
            if (health + a >= max_health)
                health = max_health;
            else
                health += a;
        }

        public virtual int Strike_at_war() {
            return impact_strength;
        }

        public virtual int Strike_at_hiler()
        {
            return impact_strength;
        }
    }
}
