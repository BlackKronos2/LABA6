using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LABA6
{
    [DataContract]
    public abstract class Statistics
    {
        [DataMember]
        public int MovesCount { get; set; }
        [DataMember]
        public int Losses1 { get; set; }
        [DataMember]
        public int Losses2 { get; set; }

        public void EndGame(int win_team) {
            Form3 Form3 = new Form3(win_team, Losses1, Losses2);
            Form3.ShowDialog();
        }
    }
}
