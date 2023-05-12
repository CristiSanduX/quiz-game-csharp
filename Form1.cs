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
            Console.WriteLine("aici");
            tabControlMain.SelectedTab = tabDomenii;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabLogin;
        }
    }
}
