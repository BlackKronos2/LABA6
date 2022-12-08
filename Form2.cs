using System;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace LABA6
{
    public partial class Form2 : Form
    {
        Battle battle;

        public Form2()
        {
            InitializeComponent();

            DataContractSerializer jsonF = new DataContractSerializer(typeof(Battle));

            using (FileStream fileStream = new FileStream("battle.json", FileMode.OpenOrCreate)) 
            {
                Battle loadbattle = (Battle)jsonF.ReadObject(fileStream);
                battle = loadbattle;
            }

            List();
            NewMove();
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

                if (textBox1.Text == string.Empty || textBox1.Text == "") throw new Exception();

                var group = (battle.Active_group_number == 1) ? (battle.Group2) : (battle.Group1);
                int count = group.L_h + group.L_w;

                battle.Attack(index);
 
                battle.HPСalculation();
                battle.Progress();
                List();
                NewMove();
                CharacterCount();
            }
            catch(Exception ex)
            {
                if (ex.Message != "")
                {
                    textBox1.Text = ex.Message;
                }
                else
                    textBox1.Text = "Ошибка!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(textBox1.Text);

                battle.Heal(index);
                battle.HPСalculation();
                battle.Progress();
                List();
                NewMove();
            }
            catch (Exception ex) {
                if (ex.Message != "")
                    textBox1.Text = "Ошибка " + ex.Message;
                else
                    textBox1.Text = "Ошибка";
            }
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

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataContractSerializer jsonF = new DataContractSerializer(typeof(Battle));

            using (FileStream fileStream = new FileStream("battle.json" , FileMode.OpenOrCreate)) 
            {
                jsonF.WriteObject(fileStream, battle);      
            }
        }
    }
}
