using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace JocQuiz
{
    public partial class Form1 : Form
    {
        private bool parolaVizibila = false;
        List<Intrebare> Intrebari = new List<Intrebare>(20);
        int indexIntrebareCurenta = 0;

        // Validare adresă de e-mail
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        // Validare parolă cu cel puțin 8 caractere
        string parolaPattern = @".{8,}";

        static string numeFisier = @"../../utilizatori.json";

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(950, 655);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            tabControlMain.Appearance = TabAppearance.FlatButtons;
            tabControlMain.ItemSize = new Size(0, 1);
            tabControlMain.SizeMode = TabSizeMode.Fixed;

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmailLogin.Text;
            string parola = textBoxParolaLogin.Text;
            try
            {
                if (AccountExists(email, parola))
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


                if (!Regex.IsMatch(email, emailPattern))
                {
                    throw new Exception("Adresa de e-mail introdusă nu este validă.");
                }
                else if (!Regex.IsMatch(parola, parolaPattern))
                {
                    throw new Exception("Parola trebuie să aibă cel puțin 8 caractere.");
                }
                else if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
                {
                    throw new Exception("Vă rugăm să completați toate câmpurile.");
                }
                else
                {
                    // Verificăm dacă fișierul utilizatori.json există deja
                    if (!File.Exists(numeFisier))
                    {
                        // Dacă nu există, creăm un nou fișier JSON cu obiectul Utilizator serializat
                        Utilizator utilizator = new Utilizator()
                        {
                            Nume = nume,
                            Email = email,
                            Parola = parola
                        };
                        //if()
                        string json = System.Text.Json.JsonSerializer.Serialize(utilizator);
                        File.WriteAllText(numeFisier, json);
                    }
                    else
                    {
                        // Dacă fișierul există deja, încărcăm datele vechi
                        string jsonVechi = File.ReadAllText(numeFisier);
                        List<Utilizator> utilizatori = System.Text.Json.JsonSerializer.Deserialize<List<Utilizator>>(jsonVechi);

                        bool utilizatorExistent = utilizatori.Any(u => u.Email == email);
                        if (utilizatorExistent)
                        {
                            throw new Exception("Exista deja un utilizator cu acest email.");
                        }
                        else
                        {
                            // Adăugăm datele noi la lista de utilizatori
                            utilizatori.Add(new Utilizator
                            {
                                Nume = nume,
                                Email = email,
                                Parola = parola
                            });

                            // Serializăm lista actualizată și rescriem fișierul JSON
                            string jsonNou = JsonConvert.SerializeObject(utilizatori, Formatting.Indented);
                            File.WriteAllText(numeFisier, jsonNou);
                        }
                    }

                    MessageBox.Show("Contul a fost creat cu succes!");
                    tabControlMain.SelectedTab = tabLogin;
                }
            }
            catch(Exception k)
            {
                MessageBox.Show(k.Message,"Eroare");
            }
        }

        private void buttonParola_Click(object sender, EventArgs e)
        {
            parolaVizibila = !parolaVizibila;

            if (parolaVizibila)
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

        public bool AccountExists(string email, string parola)
        {
            string numeFisier = @"../../utilizatori.json";
            // Verificăm dacă fișierul utilizatori.json există
            if (!File.Exists(numeFisier))
            {
                MessageBox.Show("Fișierul utilizatori.json nu există.", "Eroare");
                return false;
            }

            // Încărcăm conținutul fișierului utilizatori.json
            string json = File.ReadAllText(numeFisier);
            Console.WriteLine(json);

            // Deserializăm lista de utilizatori din fișierul JSON
            List<Utilizator> utilizatori = System.Text.Json.JsonSerializer.Deserialize<List<Utilizator>>(json);

            // Verificăm dacă există un utilizator cu email și parola introduse
            bool utilizatorExistent = utilizatori.Any(u => u.Email == email && u.Parola == parola);

            return utilizatorExistent;
        }

       
         
 

        private void buttonRaspuns1_Click(object sender, EventArgs e)
        {
        }

        private void buttonRaspuns2_Click(object sender, EventArgs e)
        {

        }

        private void buttonRaspuns3_Click(object sender, EventArgs e)
        {

        }

        private void buttonRaspuns4_Click(object sender, EventArgs e)
        {

        }

        private void buttonIstorie_Click(object sender, EventArgs e)
        {

            tabControlMain.SelectedTab = tabJoc;
            



        }

        private void buttonGeografie_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabJoc;
            Intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariGeografie.json"));
            indexIntrebareCurenta = 0; // setarea indexului întrebării curente la 0
            IncarcaIntrebare(Intrebari[indexIntrebareCurenta]);
        }

        private void buttonSport_Click(object sender, EventArgs e)
        {    
            tabControlMain.SelectedTab = tabJoc;
            Intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariSport.json"));
            indexIntrebareCurenta = 0; // setarea indexului întrebării curente la 0
            IncarcaIntrebare(Intrebari[indexIntrebareCurenta]);
        }

        private void buttonMuzica_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabJoc;
            
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
            indexIntrebareCurenta++; // increment indexul pentru a trece la următoarea întrebare
            if (indexIntrebareCurenta < Intrebari.Count)
            {
                // dacă există încă întrebări în fișierul JSON, încărcați următoarea întrebare
                IncarcaIntrebare(Intrebari[indexIntrebareCurenta]);
            }
            else
            {
                MessageBox.Show("Ati terminat jocul");
                tabControlMain.SelectedTab = tabFinal;


            }
        }
    }
}
