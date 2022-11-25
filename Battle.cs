using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA6
{
    public class Battle
    {
        Group group1;
        Group group2;
        int active_group_number;

        int moves;
        int losses1, losses2;

        public Group Group1
        {
            get { return group1; }
        }
        public Group Group2
        {
            get { return group2; }
        }
        public Battle(Group group1, Group group2)
        {
            this.group1 = group1;
            this.group2 = group2;
            this.active_group_number = 1;
            this.moves = 0;
            this.losses1 = 0;
            this.losses2 = 0;
        }
        public string List(Group group)
        {
            string log = "";
            log += "Воины: \n";

            int j = 0;
            for (int i = 0; i < group1.L_w; i++)
            {
                var war = group.War_Index(i);
                if (war.Alive)
                {
                    log += $"{i + 1}. { war.Name} {war.Health} / {war.Max_health} \n";
                    j++;
                }
            }
            log += "Целители: \n";
            for (int i = 0; i < group1.L_h; i++)
            {
                var hlr = group.Hlr_index(i);
                if (hlr.Alive)
                    log += $"{j + i + 1}. {hlr.Name} {hlr.Health} / {hlr.Max_health} \n";
            }
            return log;
        }
        public int ActiveCharapter()
        {
            var group = (active_group_number == 1) ? (Group1) : (Group2);

            return group.Active_hum_number;
        }
        public int Active_group_number
        {
            get { return active_group_number; }
        }
        public void Progress()
        {
            bool NewMove = false;
            if (active_group_number == 1)
                NewMove = group1.DoMove();
            else
                NewMove = group2.DoMove();

            if (NewMove == true)
            {
                if (active_group_number == 1)
                    active_group_number = 2;
                else
                    active_group_number = 1;
                moves++;
                if (moves % 2 == 0)
                {
                    group1 = Rec(group1);
                    group2 = Rec(group2);
                }
            }

            HPСalculation();
        }
        public void Attack(int index)
        {
            if (index <= 0) throw new Exception("Индекс меньше или равен 0");

            var group_a = (active_group_number == 1) ? (Group1) : (Group2);
            var group_d = (active_group_number != 1) ? (Group1) : (Group2);

            if (index > group_d.L_h + group_d.L_w) throw new Exception("Индекс слишком большой");

            if (index <= group_d.L_w)
            {
                if (group_a.Active_hum_number <= group_a.L_w)
                {
                    group_d.War_Index(index - 1).Damage(group_a.War_Index(group_a.Active_hum_number - 1).Strike_at_war());
                }
                else
                {
                    group_d.War_Index(index - 1).Damage(group_a.Hlr_index(group_a.Active_hum_number - group_a.L_w - 1).Strike_at_hiler());
                }
            }
            else
            {
                if (group_a.Active_hum_number <= group_a.L_w)
                {
                    group_d.Hlr_index(index - group_d.L_w - 1).Damage(group_a.War_Index(group_a.Active_hum_number - 1).Strike_at_war());
                }
                else
                {
                    group_d.Hlr_index(index - group_d.L_w - 1).Damage(group_a.Hlr_index(group_a.Active_hum_number - group_a.L_w - 1).Strike_at_hiler());
                }
            }
        }
        public bool Heal(int index)
        {
            var group = (active_group_number == 1) ? (Group1) : (Group2);

            if (index == group.Active_hum_number) throw new Exception("Нельзя лечить самого себя");

            if (index <= group.L_w)
            {
                group.War_Index(index - 1).Treatment(group.Hlr_index(group.Active_hum_number - group.L_w - 1).Impact_treatment);
            }
            else
            {
                group.Hlr_index(index - group.L_w - 1).Treatment(group.Hlr_index(group.Active_hum_number - group.L_w - 1).Impact_treatment);
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
            for (int i = 0; i < group1.L_w; i++)
                if (group1.War_Index(i).Health <= 0)
                {
                    group1.Delete_character(1, i);
                    losses1++;
                }
            for (int i = 0; i < group2.L_w; i++)
                if (group2.War_Index(i).Health <= 0)
                {
                    losses2++;
                    group2.Delete_character(1, i);
                }

            for (int i = 0; i < group1.L_h; i++)
                if (group1.Hlr_index(i).Health <= 0)
                {
                    losses1++;
                    group1.Delete_character(2, i);
                }
            for (int i = 0; i < group2.L_h; i++)
                if (group2.Hlr_index(i).Health <= 0)
                {
                    losses2++;
                    group2.Delete_character(2, i);
                }
        }
        public int Losses1
        {
            get { return losses1; }
        }
        public int Losses2
        {
            get { return losses2; }
        }

    }
}
