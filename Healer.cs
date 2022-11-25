using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA6
{
    public class Healer : Warrior
    {
        private int impact_treatment;
        public Healer()
        {
            this.impact_treatment = 0;
        }
        public Healer(string n, int max_h, int im_str, int im_treat, int rec)
        {
            name = n;
            max_health = max_h;
            health = max_health;
            impact_strength = im_str;
            impact_treatment = im_treat;
            recovery = rec;
            alive = true;
        }
        public int Impact_treatment
        {
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Значение силы исцеления персонажа-целителя меньше или равно 0");
                }
                else
                {
                    this.impact_treatment = value;
                }
            }
            get { return impact_strength; }
        }

        override public int Strike_at_war()
        {
            return impact_strength;
        }
        override public int Strike_at_hiler()
        {
            return impact_strength * 2;
        }
    }
}
