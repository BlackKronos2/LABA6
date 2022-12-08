using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA6
{
    [DataContract]
    public class Warrior: Charapter
    {
        public Warrior()
        {
            this.name = "";
            this.alive = false;
            this.health = 0;
            this.max_health = 0;
            this.impact_strength = 0;
            this.recovery = 0;
        }
        public Warrior(string n, int max_h, int im_str, int rec)
        {
            name = n;
            max_health = max_h;
            health = max_h;
            impact_strength = im_str;
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
    }
}
