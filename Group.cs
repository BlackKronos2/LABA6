using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LABA6
{
    [DataContract]
    public class Group
    {
        [DataMember]
        private const int length1 = 15;
        [DataMember]
        private const int length2 = 5;
        [DataMember]
        private Warrior[] group_war = new Warrior[length1];
        [DataMember]
        private Healer[] group_hlr = new Healer[length2];
        [DataMember]
        private int _count_warriors;
        [DataMember]
        private int _count_healers;

        [DataMember]
        private int _active_number;

        public Group(int mx_hlth_wr, int mx_hlth_hlr, int imp_strngth1, int imp_strngth2, int imp_hlr, int rec1, int rec2, int amount_war, int amount_hlr)
        {
            this._active_number = 1;
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
        public int Count_warriors
        {
            get { return _count_warriors; }
        }
        public int Count_healers
        {
            get { return _count_healers; }
        }
        public int Active_hum_number
        {
            get { return _active_number; }
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
            int index = _active_number;
            if (index <= Count_warriors)
            {
                var war = War_Index(index - 1);
                return $"{war.Name} \n Здоровье {war.Health}/{war.Max_health}\n " +
                    $"Сила удара: {war.Impact_strength}\n Естественная регенерация: {war.Recovery}";
            }
            else
            {
                index -= Count_warriors;
                var hlr = Hlr_index(index - 1);
                return $"{hlr.Name} \n Здоровье {hlr.Health}/{hlr.Max_health}\n " +
                    $"Сила удара: {hlr.Impact_strength}\n Сила регенерации: {hlr.Impact_treatment}\nЕстественная регенерация: {hlr.Recovery}";
            }
        }

        public bool DoMove()
        {
            _active_number++;
            if (_active_number > _count_warriors + _count_healers)
            {
                _active_number = 1;
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
            for (int i = 0; i < _count_warriors; i++)
                group_war[i].Treatment(group_war[i].Recovery);
            for (int i = 0; i < _count_healers; i++)
                group_hlr[i].Treatment(group_hlr[i].Recovery);
        }

        public void Delete_character(int type, int index)
        {
            if (type == 1)
            {
                group_war[index] = new Warrior();
                while (index < _count_warriors)
                {
                    index++;
                    group_war[index - 1] = group_war[index];
                }
                group_war[index - 1] = new Warrior();
                _count_warriors--;
            }
            else
            {
                group_hlr[index] = new Healer();
                while (index < _count_healers)
                {
                    index++;
                    group_hlr[index - 1] = group_hlr[index];
                }
                group_hlr[index - 1] = new Healer();
                _count_healers--;
            }
        }

        public void Add_war(string n, int mx_hlth_wr, int imp_strngth1, int rec1)
        {
            group_war[_count_warriors] = new Warrior(n, mx_hlth_wr, imp_strngth1, rec1);
            _count_warriors++;
        }
        public void Add_hlr(string n, int mx_hlth_wr, int imp_strngth1, int imp_hlr, int rec2)
        {
            group_hlr[_count_healers] = new Healer(n, mx_hlth_wr, imp_strngth1, imp_hlr, rec2);
            _count_healers++;
        }


        public void Interaction(bool d, int i, int hp)
        {
            if (d == false)
                if (i <= _count_warriors)
                {
                    if (group_war[i - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_war[i - 1] = Treatment(group_war[i - 1], hp);
                }
                else
                {
                    if (group_hlr[i - _count_warriors - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_hlr[i - _count_warriors - 1] = Treatment(group_hlr[i - _count_warriors - 1], hp);
                }
            else
            {
                if (i <= _count_warriors)
                {
                    if (group_war[i - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_war[i - 1] = Damage(group_war[i - 1], hp);
                }
                else
                {
                    if (group_hlr[i - _count_warriors - 1].Name == "") Console.WriteLine("выбран неправильный индекс");
                    group_hlr[i - _count_warriors - 1] = Damage(group_hlr[i - _count_warriors - 1], hp);
                }
            }
        }
    }
}
