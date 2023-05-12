using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JocQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabControlMain.Appearance = TabAppearance.FlatButtons;
            tabControlMain.ItemSize = new Size(0, 1);
            tabControlMain.SizeMode = TabSizeMode.Fixed;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxEmailLogin.Text == "admin" && textBoxEmailLogin.Text == "admin")
            {
                tabControlMain.SelectedTab = tabDomenii;
            }
            else
                MessageBox.Show("Nume sau parola gresita","Eroare");
           
        }

      

        private void buttonInapoiInregist_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabLogin;
        }

        private void buttonInregistrare_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabInregistrare;
        }

        private void buttonInapoiDomenii_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabLogin;
        }
    }
}
