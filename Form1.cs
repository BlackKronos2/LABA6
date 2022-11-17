using System;
using System.Windows.Forms;

namespace LABA6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int mx_hlth_wr = Convert.ToInt32(textBox1.Text);
                int mx_hlth_hlr = Convert.ToInt32(textBox2.Text);
                int imp_strngth1 = Convert.ToInt32(textBox3.Text);
                int imp_strngth2 = Convert.ToInt32(textBox4.Text);
                int imp_hlr = Convert.ToInt32(textBox5.Text);
                int rec1 = Convert.ToInt32(textBox6.Text);
                int rec2 = Convert.ToInt32(textBox7.Text);
                int amount_war = Convert.ToInt32(textBox8.Text);
                int amount_hlr = Convert.ToInt32(textBox9.Text);


                TextBox[] textBoxes = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9 };

                if (amount_war == 0 && amount_hlr == 0 || amount_war < 0 || amount_hlr < 0)
                {
                    throw new Exception("Много");
                }

                if (mx_hlth_wr < 0)
                {
                    throw new Exception("Много");
                }

                if (mx_hlth_hlr < 0)
                {
                    throw new Exception("Много");
                }

                if (imp_strngth1 < 0)
                {
                    throw new Exception("Много");
                }

                if (imp_strngth2 < 0)
                {
                    throw new Exception("Много");
                }

                if (imp_hlr < 0)
                {
                    throw new Exception("Много");
                }

                if (rec1 < 0)
                {
                    throw new Exception("Много");
                }

                if (rec2 < 0)
                {
                    throw new Exception("Много");
                }



                Group group1 = new Group(mx_hlth_wr, mx_hlth_hlr, imp_strngth1, imp_strngth2, imp_hlr, rec1, rec2, amount_war, amount_hlr);
                Group group2 = new Group(mx_hlth_wr, mx_hlth_hlr, imp_strngth1, imp_strngth2, imp_hlr, rec1, rec2, amount_war, amount_hlr);

                Form2 Form2 = new Form2(new Battle(group1, group2));
                Form2.Show();
                this.Hide();


            }
            catch
            {
                textBox1.Text = "Ошибка!";
                textBox2.Text = "Ошибка!";
                textBox3.Text = "Ошибка!";
                textBox4.Text = "Ошибка!";
                textBox5.Text = "Ошибка!";
                textBox6.Text = "Ошибка!";
                textBox7.Text = "Ошибка!";
                textBox8.Text = "Ошибка!";
                textBox9.Text = "Ошибка!";
            }
        }
    }
}