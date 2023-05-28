/*************************************************************************
* 
* File: Form1.cs *
* Descriere: Reprezintă fereastra principală a aplicației.*
* Aici sunt definite variabile private pentru gestionarea stării jocului, precum și pentru elementele 
* de interfață grafică, cum ar fi butoane, etichete și tab-uri.*
* Există metode pentru gestionarea evenimentelor apărute în interfață, cum ar fi apăsarea butoanelor*
* de login, înregistrare, selectarea unui domeniu pentru joc (Istorie, Geografie, Sport, Muzică), selectarea unui răspuns la întrebare etc.*
* De asemenea, sunt definite și metode auxiliare pentru funcționalități specifice, cum ar fi încărcarea unei întrebări sau actualizarea timpului scurs.*
* Autor: Diana-Florina Apostol*
*************************************************************************/
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
        // Campurile private sunt utilizate pentru a gestiona starea jocului

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


        /// <summary>
        /// Constructor pentru Form1.
        /// Inițializează elementele UI și setează configurațiile inițiale.
        /// </summary>
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

        /// <summary>
        /// Metoda care gestioneaza evenimentul Tick pentru _timpQuiz.
        /// Actualizează timpul scurs și afișează în label-ul corespunzător.
        /// </summary>
        private void TimpQuizTick(object sender, EventArgs e)
        {
            _timpScurs++; 
            labelTimpScurs.Text = $"Timp: {_timpScurs} secunde";
        }

        /// <summary>
        /// Metoda pentru butonul de Login.
        /// Verifică dacă contul există și navighează către tabul cu domenii de quiz.
        /// </summary>
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

        /// <summary>
        /// Metoda pentru butonul de Înregistrare.
        /// Navighează către tabul de înregistrare și curăță textbox-urile.
        /// </summary>
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

        /// <summary>
        /// Metoda pentru butonul Adauga.
        /// Crează un nou cont cu datele introduse și navighează înapoi la tabul de Login.
        /// </summary>
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


        /// <summary>
        /// Metoda pentru butonul de Parolă.
        /// Comută vizibilitatea parolei în textBoxParolaLogin și schimbă imaginea butonului în funcție de starea parolei.
        /// </summary>
        private void buttonParola_Click(object sender, EventArgs e)
        {
            // Comută starea variabilei _parolaVizibila între true și false la fiecare apăsare a butonului
            _parolaVizibila = !_parolaVizibila;

            // Dacă parola este vizibilă
            if (_parolaVizibila)
            {
                // Setează proprietatea UseSystemPasswordChar ca fiind falsă pentru a face parola vizibilă
                textBoxParolaLogin.UseSystemPasswordChar = false;
                // Schimbă imaginea butonului cu o imagine reprezentând un ochi deschis
                buttonParola.BackgroundImage = Properties.Resources.eye_open;
            }
            else // Dacă parola nu este vizibilă
            {
                // Setează proprietatea UseSystemPasswordChar ca fiind adevărată pentru a face parola invizibilă
                textBoxParolaLogin.UseSystemPasswordChar = true;
                // Schimbă imaginea butonului cu o imagine reprezentând un ochi închis
                buttonParola.BackgroundImage = Properties.Resources.eye_closed;
            }
        }


        /// <summary>
        /// Metodele pentru butoanele de Raspuns.
        /// Selectează răspunsul și schimbă culoarea butonului.
        /// </summary>
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

        /// <summary>
        /// Metodele pentru butoanele de domenii.
        /// Inițiază jocul pentru domeniul Istorie, încarcă prima întrebare, setează timpul și pornește timerul.
        /// </summary>
        internal void buttonIstorie_Click(object sender, EventArgs e)
        {
            // Setează calea către fișierul de highscore specific domeniului Istorie
            _caleHighScore = @"../../HighScoreIstorie.json";

            // Selectează tab-ul pentru joc
            tabControlMain.SelectedTab = tabJoc;

            // Inițializează obiectul _topics pentru domeniul Istorie
            _topics = new Istorie();

            // Încarcă prima întrebare în interfață
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            // Setează timpul scurs la 0 (60 de secunde disponibile)
            _timpScurs = 0;

            // Porneste timerul pentru a contoriza timpul de joc
            _timpQuiz.Start();
        }


        /// <summary>
        /// Inițiază jocul pentru domeniul Geografie, încarcă prima întrebare, setează timpul și pornește timerul.
        /// </summary>
        internal void buttonGeografie_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreGeografie.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Geografie();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        /// <summary>
        /// Inițiază jocul pentru domeniul Sport, încarcă prima întrebare, setează timpul și pornește timerul.
        /// </summary>
        private void buttonSport_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreSport.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Sport();
            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul
        }

        /// <summary>
        /// Inițiază jocul pentru domeniul Muzica, încarcă prima întrebare, setează timpul și pornește timerul.
        /// </summary>
        private void buttonMuzica_Click(object sender, EventArgs e)
        {
            _caleHighScore = @"../../HighScoreMuzica.json";
            tabControlMain.SelectedTab = tabJoc;
            _topics = new Muzica();

            IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);

            _timpScurs = 0; // Setăm timpul rămas la 60 de secunde
            _timpQuiz.Start(); // Pornim timerul

        }

        /// <summary>
        /// Metoda pentru încărcarea unei întrebări în interfață.
        /// Actualizează textul întrebării și variantele de răspuns în butoane.
        /// </summary>
        /// <param name="intrebare">Obiectul Intrebare care trebuie încărcat</param>
        public void IncarcaIntrebare(Intrebare intrebare)
        {
            // Actualizează textul întrebării în label-ul corespunzător
            labelIntrebare.Text = intrebare.intrebare;

            // Actualizează textele variantelor de răspuns în butoanele corespunzătoare
            buttonRaspuns1.Text = intrebare.variante[0];
            buttonRaspuns2.Text = intrebare.variante[1];
            buttonRaspuns3.Text = intrebare.variante[2];
            buttonRaspuns4.Text = intrebare.variante[3];
        }


        /// <summary>
        /// Metoda apelată atunci când utilizatorul dă clic pe butonul "Trimite Răspuns".
        /// Verifică răspunsul ales de utilizator și actualizează interfața în consecință.
        /// </summary>
        /// <param name="sender">Obiectul care a generat evenimentul</param>
        /// <param name="e">Argumentele evenimentului</param>
        private void buttonTrimiteRaspuns_Click(object sender, EventArgs e)
        {
            if (_topics.intrebari[_topics.indexIntrebareCurenta].raspuns == _raspunsAles)
            {
                _raspunsuriCorecte++; // Incrementăm numărul de răspunsuri corecte dacă răspunsul ales este corect
            }

            if (++(_topics.indexIntrebareCurenta) < _topics.intrebari.Count) // Dacă mai există întrebări, o încărcăm pe următoarea
            {
                // Resetăm culorile butoanelor de răspuns la alb
                buttonRaspuns2.BackColor = Color.White;
                buttonRaspuns1.BackColor = Color.White;
                buttonRaspuns3.BackColor = Color.White;
                buttonRaspuns4.BackColor = Color.White;

                // Încărcăm următoarea întrebare
                IncarcaIntrebare(_topics.intrebari[_topics.indexIntrebareCurenta]);
                _raspunsAles = "";
            }
            else // Dacă nu mai există întrebări, jocul s-a terminat
            {
                // Selectăm tab-ul pentru afișarea scorului final
                tabControlMain.SelectedTab = tabFinal;

                // Actualizăm textul pentru scorul final și timpul
                labelScor.Text = $"Scor final: {_raspunsuriCorecte}/20 răspunsuri corecte.";
                labelTimp.Text = $"{_timpScurs} secunde";

                // Resetăm scorul și notificăm observatorii (HighScoreObserver)
                _score.NotifyObservers(_raspunsuriCorecte, _timpScurs, _nume, _caleHighScore);

                // Afișăm tabela de punctaj
                ShowTabelaPunctaj();

                // Resetăm numărul de răspunsuri corecte
                _raspunsuriCorecte = 0;
            }
        }


        internal void buttonJocNou_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabDomenii;
        }

        /// <summary>
        /// Metoda pentru afișarea tabelului de punctaj.
        /// </summary>
        private void ShowTabelaPunctaj()
        {
            try
            {
                if (!File.Exists(_caleHighScore))
                {
                    throw new Exception("Fișierul " + _caleHighScore + ".json nu există.");
                }

                // Citim conținutul fișierului JSON
                string json = File.ReadAllText(_caleHighScore);

                // Deserializăm conținutul într-o listă de obiecte de tipul Score
                List<Score> scoruri = System.Text.Json.JsonSerializer.Deserialize<List<Score>>(json);

                // Sortăm scorurile descrescător după scor și apoi crescător după timp
                scoruri = scoruri.OrderByDescending(s => s.scor).ThenBy(s => s.timp).ToList();

                // Construim textul tabelului de punctaj
                string text = "Nume" + "   " + "Scor" + "    " + "Timp\n";
                int count = 5;
                foreach (var scor in scoruri)
                {
                    if (count == 0)
                        break;
                    text += scor.nume + "   " + scor.scor + "            " + scor.timp;
                    text += '\n';
                    count--;
                }

                // Afișăm textul în eticheta labelHighScore
                labelHighScore.Text = text;
            }
            catch (Exception k)
            {
                MessageBox.Show(k.Message, "Eroare");
            }
        }

        /// <summary>
        /// Funcție pentru evenimentul de click al butonului "About".
        /// Afișează o casetă de mesaj cu informații despre jocul de cultură generală.
        /// </summary>
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            // Afiseaza o caseta de mesaj cu informatii despre jocul de cultura generala
            MessageBox.Show("Bun venit la Jocul de Cultură Generală! Acest joc captivant îți testează cunoștințele într-o varietate de domenii precum istorie, geografie, muzică și sport. Răspunde corect la întrebări pentru a acumula puncte și a avansa în clasament. Fii pregătit să îți pui mintea la contribuție și să îți testezi cultura generală într-un mod distractiv și educativ!\n\n© 2023 Apostol Diana, Ciobanu Eduard, Miron Sebastian, Sandu Cristi", "About");
        }
    }
}
