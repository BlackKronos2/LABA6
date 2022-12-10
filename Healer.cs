using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA6
{
    [DataContract]
    public class Healer : Charapter
    {
        [DataMember]
        private int _impact_treatment;
        public Healer()
        {
            this._impact_treatment = 0;
        }
        public Healer(string n, int max_h, int im_str, int im_treat, int rec)
        {
            _name = n;
            _max_health = max_h;
            _health = _max_health;
            _impact_strength = im_str;
            _impact_treatment = im_treat;
            _recovery = rec;
            _alive = true;
        }
        override public int Strike_at_war()
        {
            return _impact_strength;
        }
        override public int Strike_at_hiler()
        {
            return _impact_strength * 2;
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
                    this._impact_treatment = value;
                }
            }
            get { return _impact_strength; }
        }
    }
}
