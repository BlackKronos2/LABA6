using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace LABA6
{
    [DataContract]
    public abstract class Charapter
    {
        [DataMember]
        protected string _name;
        [DataMember]
        protected int _health;
        [DataMember]
        protected int _max_health;
        [DataMember]
        protected int _impact_strength;
        [DataMember]
        protected int _recovery;
        [DataMember]
        protected bool _alive;

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
                    this._name = value;
                }
            }
            get { return _name; }
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
                    this._max_health = value;
                    this._health = value;
                }
            }
            get { return _max_health; }
        }
        public int Health
        {
            get { return _health; }
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
                    this._impact_strength = value;
                }
            }
            get { return _impact_strength; }
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
                    this._recovery = value;
                }
            }
            get { return _recovery; }
        }
        public bool Alive
        {
            set { this._alive = value; }
            get { return _alive; }
        }
        public void Damage(int a)
        {
            if (_health - a <= 0)
                _health = 0;
            else
                _health -= a;
        }
        public void Treatment(int a)
        {
            if (_health + a >= _max_health)
                _health = _max_health;
            else
                _health += a;
        }
        public virtual int Strike_at_war() {
            return _impact_strength;
        }
        public virtual int Strike_at_hiler()
        {
            return _impact_strength;
        }
    }
}
