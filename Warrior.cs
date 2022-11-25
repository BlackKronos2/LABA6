using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA6
{
    public class Warrior: Charapter
    {
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
