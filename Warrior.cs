using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA6
{
    [DataContract]
    public class Warrior: Charapter
    {
        public Warrior()
        {
            _name = "";
            _alive = false;
            _health = 0;
            _max_health = 0;
            _impact_strength = 0;
            _recovery = 0;
        }
        public Warrior(string n, int max_h, int im_str, int rec)
        {
            _name = n;
            _max_health = max_h;
            _health = max_h;
            _impact_strength = im_str;
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
    }
}
