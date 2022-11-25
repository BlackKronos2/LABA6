using System;

namespace LABA6
{
    public class Group
    {
        private const int l1 = 15;
        private const int l2 = 5;
        private Warrior[] group_war = new Warrior[l1];
        private Healer[] group_hlr = new Healer[l2];
        private int l_w;
        private int l_h;

        private int active_hum_number;

        public Group(int mx_hlth_wr, int mx_hlth_hlr, int imp_strngth1, int imp_strngth2, int imp_hlr, int rec1, int rec2, int amount_war, int amount_hlr)
        {
            this.active_hum_number = 1;
            for (int i = 0; i < amount_war; i++)
            {
                string name = $"Warrior{i + 1}";
                this.Add_war(name, mx_hlth_wr, imp_strngth1, rec1);
            }
            for (int i = 0; i < amount_hlr; i++)
            {
                string name = $"Healer{i + 1}";
                this.Add_hlr(name, mx_hlth_hlr, imp_strngth2, imp_hlr, rec2);
            }
        }
        public int L_w
        {
            get { return l_w; }
        }
        public int L_h
        {
            get { return l_h; }
        }
        public int Active_hum_number
        {
            get { return active_hum_number; }
        }
        public Warrior War_Index(int index)
        {
            return group_war[index];
        }
        public Healer Hlr_index(int index)
        {
            return group_hlr[index];
        }
        public string GetStat()
        {
            int index = active_hum_number;
            if (index <= L_w)
            {
                var war = War_Index(index - 1);
                return $"{war.Name} \n Здоровье {war.Health}/{war.Max_health}\n " +
                    $"Сила удара: {war.Impact_strength}\n Естественная регенерация: {war.Recovery}";
            }
            else
            {
                index -= L_w;
                var hlr = Hlr_index(index - 1);
                return $"{hlr.Name} \n Здоровье {hlr.Health}/{hlr.Max_health}\n " +
                    $"Сила удара: {hlr.Impact_strength}\n Сила регенерации: {hlr.Impact_treatment}\nЕстественная регенерация: {hlr.Recovery}";
            }
        }

        public bool DoMove()
        {
            active_hum_number++;
            if (active_hum_number > l_w + l_h)
            {
                active_hum_number = 1;
                return true;
            }
            return false;
        }
        public Warrior Damage(Warrior target, int hp)
        {
            target.Damage(hp);
            return target;
        }
        public Healer Damage(Healer target, int hp)
        {
            target.Damage(hp);
            return target;
        }
        public Warrior Treatment(Warrior target, int hp)
        {
            target.Treatment(hp);
            return target;
        }
        public Healer Treatment(Healer target, int hp)
        {
            target.Treatment(hp);
            return target;
        }

        public void Recovery()
        {
            for (int i = 0; i < l_w; i++)
                group_war[i].Treatment(group_war[i].Recovery);
            for (int i = 0; i < l_h; i++)
                group_hlr[i].Treatment(group_hlr[i].Recovery);
        }

        public void Delete_character(int type, int index)
        {
            if (type == 1)
            {
                group_war[index] = new Warrior();
                while (index < l_w)
                {
                    index++;
                    group_war[index - 1] = group_war[index];
                }
                group_war[index - 1] = new Warrior();
                l_w--;
            }
            else
            {
                group_hlr[index] = new Healer();
                while (index < l_h)
                {
                    index++;
                    group_hlr[index - 1] = group_hlr[index];
                }
                group_hlr[index - 1] = new Healer();
                l_h--;
            }
        }

        public void Add_war(string n, int mx_hlth_wr, int imp_strngth1, int rec1)
        {
            group_war[l_w] = new Warrior(n, mx_hlth_wr, imp_strngth1, rec1);
            l_w++;
        }
        public void Add_hlr(string n, int mx_hlth_wr, int imp_strngth1, int imp_hlr, int rec2)
        {
            group_hlr[l_h] = new Healer(n, mx_hlth_wr, imp_strngth1, imp_hlr, rec2);
            l_h++;
        }


        public void Interaction(bool d, int i, int hp)
        {
            if (d == false)
                if (i <= l_w)
                {
                    if (group_war[i - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_war[i - 1] = Treatment(group_war[i - 1], hp);
                }
                else
                {
                    if (group_hlr[i - l_w - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_hlr[i - l_w - 1] = Treatment(group_hlr[i - l_w - 1], hp);
                }
            else
            {
                if (i <= l_w)
                {
                    if (group_war[i - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_war[i - 1] = Damage(group_war[i - 1], hp);
                }
                else
                {
                    if (group_hlr[i - l_w - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_hlr[i - l_w - 1] = Damage(group_hlr[i - l_w - 1], hp);
                }
            }
        }
    }
}
