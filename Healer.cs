using System;

namespace LABA6
{
    public class Healer : Charapter
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
        override public int Strike_at_war()
        {
            return impact_strength;
        }
        override public int Strike_at_hiler()
        {
            return impact_strength * 2;
        }
        public int Impact_treatment
        {
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Значение силы исцеления персонажа-целителя меньше или равно 0");
                }
                else
                {
                    this.impact_treatment = value;
                }
            }
            get { return impact_strength; }
        }
    }
}
