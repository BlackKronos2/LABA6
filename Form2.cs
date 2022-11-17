using System;
using System.Windows.Forms;

namespace LABA6
{
    public partial class Form2 : Form
    {
        Battle battle;

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Battle a)
        {
            InitializeComponent();
            richTextBox1.Enabled = false;
            richTextBox2.Enabled = false;
            richTextBox3.Enabled = false;
            battle = a;
            List();
            NewMove();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void List()
        {
            richTextBox1.Text = battle.List(battle.Group1);
            richTextBox2.Text = battle.List(battle.Group2);
        }

        private void NewMove()
        {
            var group = (battle.Active_group_number == 1) ? (battle.Group1) : (battle.Group2);

            richTextBox3.Text = $"Группа {battle.Active_group_number}\n" + group.GetStat();
            if (group.Active_hum_number <= group.L_w)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            battle.Progress();
            battle.HPСalculation();
            List();
            NewMove();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(textBox1.Text);

                var group = (battle.Active_group_number == 1) ? (battle.Group2) : (battle.Group1);
                int count = group.L_h + group.L_w;

                if (index > count)
                {
                    throw new Exception("Много");
                }
                textBox1.Text = (battle.Attack(index)) ? ("") : ("Неверный индекс");
                battle.HPСalculation();
                battle.Progress();
                List();
                NewMove();
                CharacterCount();
            }
            catch
            {
                textBox1.Text = "Ошибка!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(textBox1.Text);

            textBox1.Text = (battle.Heal(index)) ? ("") : ("Неверный индекс");
            battle.HPСalculation();
            battle.Progress();
            List();
            NewMove();

        }

        private void CharacterCount()
        {
            if ((battle.Group1.L_w + battle.Group1.L_h == 0))
            {
                Form3 Form3 = new Form3(2, battle.Losses1, battle.Losses2);
                Form3.Show();
                this.Hide();
            }
            else if (battle.Group2.L_w + battle.Group2.L_h == 0)
            {
                Form3 Form3 = new Form3(1, battle.Losses1, battle.Losses2);
                Form3.Show();
                this.Hide();
            }
        }
    }
    public class Warrior
    {
        protected string name;
        protected int health;
        protected int max_health;
        protected int impact_strength;
        protected int recovery;
        protected bool alive;

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
        public virtual int Strike_at_war()
        {
            return impact_strength;
        }
        public virtual int Strike_at_hiler()
        {
            return impact_strength * 2;
        }
    }
    public class Healer : Warrior
    {
        private int impact_treatment;
        public Healer()
        {
            this.impact_treatment = 0;
        }
        public Healer(string n, int max_h, int im_str, int im_treat, int rec)
        {
            name = n;
            max_health = max_h;
            health = max_health;
            impact_strength = im_str;
            impact_treatment = im_treat;
            recovery = rec;
            alive = true;
        }
        public int Impact_treatment
        {
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Значение силы исцеления персонажа-целителя меньше или равно 0");
                }
                else
                {
                    this.impact_treatment = value;
                }
            }
            get { return impact_strength; }
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
                return $"Сейчас ходит: \n {war.Name} \n Здоровье {war.Health}/{war.Max_health}\n " +
                    $"Сила удара: {war.Impact_strength}\n Естественная регенерация: {war.Recovery}";
            }
            else
            {
                index -= L_w;
                var hlr = Hlr_index(index - 1);
                return $"Сейчас ходит: \n {hlr.Name} \n Здоровье {hlr.Health}/{hlr.Max_health}\n " +
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
        public bool Attack(int index)
        {
            var group_a = (active_group_number == 1) ? (Group1) : (Group2);
            var group_d = (active_group_number != 1) ? (Group1) : (Group2);

            if (index <= group_d.L_w)
            {
                if (group_a.Active_hum_number <= group_a.L_w)
                {
                    group_d.War_Index(index - 1).Damage(group_a.War_Index(group_a.Active_hum_number - 1).Strike_at_war());
                    return true;
                }
                else
                {
                    group_d.War_Index(index - 1).Damage(group_a.Hlr_index(group_a.Active_hum_number - group_a.L_w - 1).Strike_at_hiler());
                    return true;
                }
            }
            else
            {
                if (group_a.Active_hum_number <= group_a.L_w)
                {
                    group_d.Hlr_index(index - group_d.L_w - 1).Damage(group_a.War_Index(group_a.Active_hum_number - 1).Strike_at_war());
                    return true;
                }
                else
                {
                    group_d.Hlr_index(index - group_d.L_w - 1).Damage(group_a.Hlr_index(group_a.Active_hum_number - group_a.L_w - 1).Strike_at_hiler());
                    return true;
                }
            }
        }
        public bool Heal(int index)
        {
            var group = (active_group_number == 1) ? (Group1) : (Group2);

            if ((index - 1) == group.Active_hum_number) return false;

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
