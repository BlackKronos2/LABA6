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

            using (FileStream fileStream = new FileStream("battle.json", FileMode.Open)) 
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
            if (group.Active_hum_number <= group.Count_warriors)
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
                int count = group.Count_healers + group.Count_warriors;

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
            if ((battle.Group1.Count_warriors + battle.Group1.Count_healers == 0))
                battle.EndGame(2);
            else if (battle.Group2.Count_warriors + battle.Group2.Count_healers == 0)
                battle.EndGame(1);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Enabled = richTextBox2.Enabled = richTextBox3.Enabled = false;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Load = true;
            Properties.Settings.Default.Save();

            DataContractSerializer jsonF = new DataContractSerializer(typeof(Battle));

            using (FileStream fileStream = new FileStream("battle.json" , FileMode.Create)) 
            {
                jsonF.WriteObject(fileStream, battle);      
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Load = true;
            Properties.Settings.Default.Save();

            DataContractSerializer jsonF = new DataContractSerializer(typeof(Battle));

            using (FileStream fileStream = new FileStream("battle.json", FileMode.Create))
            {
                jsonF.WriteObject(fileStream, battle);
            }

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
