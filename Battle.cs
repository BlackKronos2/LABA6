using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA6
{
    [DataContract]
    public class Battle: Statistics
    {
        [DataMember]
        public Group Group1 { get; set; }
        [DataMember]
        public Group Group2 { get; set; }
        [DataMember]
        private int _active_group_number;

        public Battle(Group Group1, Group Group2)
        {
            this.Group1 = Group1;
            this.Group2 = Group2;
            this._active_group_number = 1;
            this.MovesCount = 0;
            this.Losses1 = 0;
            this.Losses2 = 0;
        }
        public string List(Group group)
        {
            string log = "";
            log += "Воины: \n";

            int j = 0;
            for (int i = 0; i < group.Count_warriors; i++)
            {
                var war = group.War_Index(i);
                if (war.Alive)
                {
                    log += $"{i + 1}. { war.Name} {war.Health} / {war.Max_health} \n";
                    j++;
                }
            }
            log += "Целители: \n";
            for (int i = 0; i < group.Count_healers; i++)
            {
                var hlr = group.Hlr_index(i);
                if (hlr.Alive)
                    log += $"{j + i + 1}. {hlr.Name} {hlr.Health} / {hlr.Max_health} \n";
            }
            return log;
        }
        public int Active_group_number
        {
            get { return _active_group_number; }
        }
        public void Progress()
        {
            bool NewMove = (_active_group_number == 1) ? (Group1.DoMove()):(Group2.DoMove());

            if (NewMove == true)
            {
                if (_active_group_number == 1)
                    _active_group_number = 2;
                else
                    _active_group_number = 1;
                MovesCount++;

                if (MovesCount % 2 == 0)
                {
                    Group1 = Rec(Group1);
                    Group2 = Rec(Group2);
                }
            }

            HPСalculation();
        }
        public void Attack(int index)
        {
            if (index <= 0) throw new Exception("Индекс меньше или равен 0");

            var group_a = (_active_group_number == 1) ? (Group1) : (Group2);
            var group_d = (_active_group_number != 1) ? (Group1) : (Group2);

            if (index > group_d.Count_healers + group_d.Count_warriors) throw new Exception("Индекс слишком большой");

            if (index <= group_d.Count_warriors)
            {
                if (group_a.Active_hum_number <= group_a.Count_warriors)
                {
                    group_d.War_Index(index - 1).Damage(group_a.War_Index(group_a.Active_hum_number - 1).Strike_at_war());
                }
                else
                {
                    group_d.War_Index(index - 1).Damage(group_a.Hlr_index(group_a.Active_hum_number - group_a.Count_warriors - 1).Strike_at_hiler());
                }
            }
            else
            {
                if (group_a.Active_hum_number <= group_a.Count_warriors)
                {
                    group_d.Hlr_index(index - group_d.Count_warriors - 1).Damage(group_a.War_Index(group_a.Active_hum_number - 1).Strike_at_war());
                }
                else
                {
                    group_d.Hlr_index(index - group_d.Count_warriors - 1).Damage(group_a.Hlr_index(group_a.Active_hum_number - group_a.Count_warriors - 1).Strike_at_hiler());
                }
            }
        }
        public bool Heal(int index)
        {
            var group = (_active_group_number == 1) ? (Group1) : (Group2);

            if (index == group.Active_hum_number) throw new Exception("Нельзя лечить самого себя");

            if (index <= group.Count_warriors)
            {
                group.War_Index(index - 1).Treatment(group.Hlr_index(group.Active_hum_number - group.Count_warriors - 1).Impact_treatment);
            }
            else
            {
                group.Hlr_index(index - group.Count_warriors - 1).Treatment(group.Hlr_index(group.Active_hum_number - group.Count_warriors - 1).Impact_treatment);
            }
            return true;
        }
        public Group Rec(Group target)
        {
            target.Recovery();
            return target;
        }
        public void HPСalculation()
        {
            for (int i = 0; i < Group1.Count_warriors; i++)
                if (Group1.War_Index(i).Health <= 0)
                {
                    Group1.Delete_character(1, i);
                    Losses1++;
                }
            for (int i = 0; i < Group2.Count_warriors; i++)
                if (Group2.War_Index(i).Health <= 0)
                {
                    Losses2++;
                    Group2.Delete_character(1, i);
                }

            for (int i = 0; i < Group1.Count_healers; i++)
                if (Group1.Hlr_index(i).Health <= 0)
                {
                    Losses1++;
                    Group1.Delete_character(2, i);
                }
            for (int i = 0; i < Group2.Count_healers; i++)
                if (Group2.Hlr_index(i).Health <= 0)
                {
                    Losses2++;
                    Group2.Delete_character(2, i);
                }
        }

    }
}
