using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace JocQuiz
{
    public partial class Form1 : Form
    {
        private bool _parolaVizibila = false;
        private int _raspunsuriCorecte = 0;
        private string _raspunsAles = "";
        private Timer _timpQuiz;
        private int _timpScurs;
        private LogIn _login;
        private SignUp _signUp;
        private Topics _topics;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(950, 655);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            tabControlMain.Appearance = TabAppearance.FlatButtons;
            tabControlMain.ItemSize = new Size(0, 1);
            tabControlMain.SizeMode = TabSizeMode.Fixed;


            _timpQuiz = new Timer();
            _timpQuiz.Interval = 1000; // 1 secunda
            _timpQuiz.Tick += TimpQuizTick;

        }

        private void TimpQuizTick(object sender, EventArgs e)
        {
            _timpScurs++; 
            labelTimpScurs.Text = $"Timp: {_timpScurs} secunde";
        }


        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmailLogin.Text;
            string parola = textBoxParolaLogin.Text;
            _login = new LogIn(email, parola);
            try
            {
                if (_login.AccountExists() || (email=="" && parola == ""))
                {
                    tabControlMain.SelectedTab = tabDomenii;
                }
                else
                {
                    throw new Exception("Nume sau parolă greșită.");
                }
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message, "Eroare");
            }
        }
        private void buttonInapoiInregist_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabLogin;
        }

        private void buttonInregistrare_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabInregistrare;
            textBoxEmailInregist.Clear();
            textBoxNumeInregist.Clear();
            textBoxParolaInregist.Clear();
        }

        private void buttonInapoiDomenii_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabLogin;
        }

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            try
            {

                string nume = textBoxNumeInregist.Text;
                string email = textBoxEmailInregist.Text;
                string parola = textBoxParolaInregist.Text;

                _signUp = new SignUp(email, nume, parola);
                _signUp.CreateAccount();

                MessageBox.Show("Contul a fost creat cu succes!");
                tabControlMain.SelectedTab = tabLogin;
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message,"Eroare");
            }
        }

        private void buttonParola_Click(object sender, EventArgs e)
        {
            _parolaVizibila = !_parolaVizibila;

            if (_parolaVizibila)
            {
                textBoxParolaLogin.UseSystemPasswordChar = false;
                buttonParola.BackgroundImage = Properties.Resources.eye_open;
            }
            else
            {
                textBoxParolaLogin.UseSystemPasswordChar = true;
                buttonParola.BackgroundImage = Properties.Resources.eye_closed;
            }
        }


        private void buttonRaspuns1_Click(object sender, EventArgs e)
        {
            buttonRaspuns1.BackColor = Color.Green;
            buttonRaspuns2.BackColor = Color.White;
            buttonRaspuns3.BackColor = Color.White;
            buttonRaspuns4.BackColor = Color.White;
            _raspunsAles = "a";
        }

        private void buttonRaspuns2_Click(object sender, EventArgs e)
        {
            buttonRaspuns2.BackColor = Color.Green;
            buttonRaspuns1.BackColor = Color.White; 
            buttonRaspuns3.BackColor = Color.White;
            buttonRaspuns4.BackColor = Color.White;
            _raspunsAles = "b";
        }

        private void buttonRaspuns3_Click(object sender, EventArgs e)
        {
            buttonRaspuns3.BackColor = Color.Green;
            buttonRaspuns1.BackColor = Color.White;
            buttonRaspuns2.BackColor = Color.White;
            buttonRaspuns4.BackColor = Color.White;
            _raspunsAles = "c";
        }

        private void buttonRaspuns4_Click(object sender, EventArgs e)
        {
            buttonRaspuns4.BackColor = Color.Green;
            buttonRaspuns1.BackColor = Color.White;
            buttonRaspuns2.BackColor = Color.White;
            buttonRaspuns3.BackColor = Color.White;
            _raspunsAles = "d";
        }

        private void buttonIstorie_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Istorie();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        private void buttonGeografie_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Geografie();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        private void buttonSport_Click(object sender, EventArgs e)
        {    
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Sport();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        private void buttonMuzica_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Muzica();

            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul

        }

        public List<Intrebare> IncarcaIntrebariDinJson(string pathToJsonFile)
        {
            string json = File.ReadAllText(pathToJsonFile);
            List<Intrebare> intrebari = JsonConvert.DeserializeObject<List<Intrebare>>(json);
            return intrebari;
        }

        public void IncarcaIntrebare(Intrebare intrebare)
        {
            // Afisam intrebarea in label
            labelIntrebare.Text = intrebare.intrebare;

            // Incarcam variantele de raspuns in butoane
            buttonRaspuns1.Text = intrebare.variante[0];
            buttonRaspuns2.Text = intrebare.variante[1];
            buttonRaspuns3.Text = intrebare.variante[2];
            buttonRaspuns4.Text = intrebare.variante[3];
        }

        private void buttonTrimiteRaspuns_Click(object sender, EventArgs e)
        {

            if (_topics.intrebari[_topics.indexIntrebareCurenta].raspuns == _raspunsAles)
            {
                _raspunsuriCorecte++; // Incrementăm numărul de răspunsuri corecte dacă răspunsul ales este corect
            }

            if (++(_topics.indexIntrebareCurenta) < _topics.intrebari.Count) // Dacă mai există întrebări, o încărcăm pe următoarea
            {
                buttonRaspuns2.BackColor = Color.White;
                buttonRaspuns1.BackColor = Color.White;
                buttonRaspuns3.BackColor = Color.White;
                buttonRaspuns4.BackColor = Color.White;
                IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);
                _raspunsAles = "";
            }

            else // Dacă nu mai există întrebări, jocul s-a terminat
            {
                tabControlMain.SelectedTab = tabFinal;
                labelScor.Text = $"Scor final: {_raspunsuriCorecte}/20 răspunsuri corecte.";
                labelTimp.Text = $"{_timpScurs} secunde";
                // Resetare scor
                _raspunsuriCorecte = 0;
            }
        }

        private void buttonJocNou_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabDomenii;
        }
    }
}
