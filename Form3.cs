using System;
using System.Windows.Forms;

namespace LABA6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(int group_win, int losses1, int losses2)
        {
            InitializeComponent();
            label3.Text += (group_win == 1) ? (" Группа 1") : (" Группа 2");
            label5.Text = losses1.ToString();
            label6.Text = losses2.ToString();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Load = false;
            Properties.Settings.Default.Save();
        }
    }
}
