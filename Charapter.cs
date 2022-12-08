using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace LABA6
{
    [DataContract]
    public class Charapter
    {
        [DataMember]
        protected string name;
        [DataMember]
        protected int health;
        [DataMember]
        protected int max_health;
        [DataMember]
        protected int impact_strength;
        [DataMember]
        protected int recovery;
        [DataMember]
        protected bool alive;

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
