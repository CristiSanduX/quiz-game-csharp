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

        // Validare adresă de e-mail
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        // Validare parolă cu cel puțin 8 caractere
        string parolaPattern = @".{8,}";


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

            string numeFisier = "utilizatori.json";
            string caleFisier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, numeFisier);

            // Verificăm dacă fișierul utilizatori.json există
            if (!File.Exists(caleFisier))
            {
                MessageBox.Show("Fișierul utilizatori.json nu există.", "Eroare");
                return;
            }

            // Încărcăm conținutul fișierului utilizatori.json
            string json = File.ReadAllText(caleFisier);

            // Deserializăm lista de utilizatori din fișierul JSON
            List<Utilizator> utilizatori = System.Text.Json.JsonSerializer.Deserialize<List<Utilizator>>(json);

            // Verificăm dacă există un utilizator cu numele și parola introduse
            bool utilizatorExistent = utilizatori.Any(u => u.Email == email && u.Parola == parola);

            if (utilizatorExistent)
            {
                tabControlMain.SelectedTab = tabDomenii;
            }
            else
            {
                MessageBox.Show("Nume sau parolă greșită.", "Eroare");
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
            string nume = textBoxNumeInregist.Text;
            string email = textBoxEmailInregist.Text;
            string parola = textBoxParolaInregist.Text;

            string numeFisier = "utilizatori.json";
            string caleFisier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, numeFisier);


            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Adresa de e-mail introdusă nu este validă.");
            }
            else if (!Regex.IsMatch(parola, parolaPattern))
            {
                MessageBox.Show("Parola trebuie să aibă cel puțin 8 caractere.");
            }
            else if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Vă rugăm să completați toate câmpurile.");
            }
            else
            {
                // Verificăm dacă fișierul utilizatori.json există deja
                if (!File.Exists(caleFisier))
                {
                    // Dacă nu există, creăm un nou fișier JSON cu obiectul Utilizator serializat
                    Utilizator utilizator = new Utilizator()
                    {
                        Nume = nume,
                        Email = email,
                        Parola = parola
                    };
                    string json = System.Text.Json.JsonSerializer.Serialize(utilizator);
                    File.WriteAllText(caleFisier, json);
                }
                else
                {
                    // Dacă fișierul există deja, încărcăm datele vechi
                    string jsonVechi = File.ReadAllText(caleFisier);
                    List<Utilizator> utilizatori = System.Text.Json.JsonSerializer.Deserialize<List<Utilizator>>(jsonVechi);

                    
                    // Adăugăm datele noi la lista de utilizatori
                    utilizatori.Add(new Utilizator
                    {
                        Nume = nume,
                        Email = email,
                        Parola = parola
                    });

                    // Serializăm lista actualizată și rescriem fișierul JSON
                    string jsonNou = JsonConvert.SerializeObject(utilizatori, Formatting.Indented);
                    File.WriteAllText(@"C:\Users\cioba\Desktop\Proiect IP NOU\ProiectIP\utilizatori.json", jsonNou);
                }

                MessageBox.Show("Contul a fost creat cu succes!");
                tabControlMain.SelectedTab = tabLogin;
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

        
    }
}
