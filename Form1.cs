using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Login;

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
        private Score _score = new Score();
        private HighScoreObserver _highScoreObserver = new HighScoreObserver();
        private string _caleHighScore;

        private string _nume;

        public string GetIntrebareText()
        {
            return labelIntrebare.Text;
        }
        public string GetRaspunsText(int numarButon)
        {
            switch (numarButon)
            {
                case 1:
                    return buttonRaspuns1.Text;
                case 2:
                    return buttonRaspuns2.Text;
                case 3:
                    return buttonRaspuns3.Text;
                case 4:
                    return buttonRaspuns4.Text;
                default:
                    throw new ArgumentException("Numărul butonului trebuie să fie între 1 și 4.");
            }
        }
        public string GetRaspunsAles()
        {
            return _raspunsAles;
        }


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

            _score.RegisterObserver(_highScoreObserver);
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
                    _nume = _login.nume;
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
        internal void buttonInapoiInregist_Click(object sender, EventArgs e)
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

        internal void buttonInapoiDomenii_Click(object sender, EventArgs e)
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

                _signUp = new SignUp(email, _nume, parola);
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


        internal void buttonRaspuns1_Click(object sender, EventArgs e)
        {
            buttonRaspuns1.BackColor = Color.Green;
            buttonRaspuns2.BackColor = Color.White;
            buttonRaspuns3.BackColor = Color.White;
            buttonRaspuns4.BackColor = Color.White;
            _raspunsAles = "a";
        }

        internal void buttonRaspuns2_Click(object sender, EventArgs e)
        {
            buttonRaspuns2.BackColor = Color.Green;
            buttonRaspuns1.BackColor = Color.White; 
            buttonRaspuns3.BackColor = Color.White;
            buttonRaspuns4.BackColor = Color.White;
            _raspunsAles = "b";
        }

        internal void buttonRaspuns3_Click(object sender, EventArgs e)
        {
            buttonRaspuns3.BackColor = Color.Green;
            buttonRaspuns1.BackColor = Color.White;
            buttonRaspuns2.BackColor = Color.White;
            buttonRaspuns4.BackColor = Color.White;
            _raspunsAles = "c";
        }

        internal void buttonRaspuns4_Click(object sender, EventArgs e)
        {
            buttonRaspuns4.BackColor = Color.Green;
            buttonRaspuns1.BackColor = Color.White;
            buttonRaspuns2.BackColor = Color.White;
            buttonRaspuns3.BackColor = Color.White;
            _raspunsAles = "d";
        }

        internal void buttonIstorie_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreIstorie.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Istorie();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        internal void buttonGeografie_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreGeografie.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Geografie();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        private void buttonSport_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreSport.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Sport();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        private void buttonMuzica_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreMuzica.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Muzica();

            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul

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
                _score.NotifyObservers(_raspunsuriCorecte, _timpScurs, _nume, _caleHighScore);
                ShowTabelaPunctaj();
                _raspunsuriCorecte = 0;
            }
        }

        internal void buttonJocNou_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabDomenii;
        }

        private void ShowTabelaPunctaj() 
        {
            try
            {
                if (!File.Exists(_caleHighScore))
                {
                    throw new Exception("Fișierul "+_caleHighScore+".json nu există.");
                }
                string json = File.ReadAllText(_caleHighScore);

                List<Score> scoruri = System.Text.Json.JsonSerializer.Deserialize<List<Score>>(json);
                scoruri = scoruri.OrderByDescending(s => s.scor).ThenBy(s => s.timp).ToList();
                string text = "Nume"+ "   " + "Scor" + "    " + "Timp\n";
                int count = 5;
                foreach(var scor in scoruri)
                {
                    if (count == 0)
                        break;
                    text += scor.nume + "   " + scor.scor + "            " + scor.timp;
                    text += '\n';
                    count--;
                }
                labelHighScore.Text = text;
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message, "Eroare");
            }
        }

        private void labelTimpScurs_Click(object sender, EventArgs e)
        {

        }
    }
}
