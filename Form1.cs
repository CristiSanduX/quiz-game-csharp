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

        private void buttonAdauga_Click(object sender, EventArgs e)
        {
            string nume = textBoxNumeInregist.Text;
            string email = textBoxEmailInregist.Text;
            string parola = textBoxParolaInregist.Text;

            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Vă rugăm să completați toate câmpurile.");
            }
            else
            {
                // Verificăm dacă fișierul utilizatori.json există deja
                if (!File.Exists(@"C:\Users\cioba\Desktop\Proiect IP\ProiectIP\utilizatori.json"))
                {
                    // Dacă nu există, creăm un nou fișier JSON cu obiectul Utilizator serializat
                    Utilizator utilizator = new Utilizator()
                    {
                        Nume = nume,
                        Email = email,
                        Parola = parola
                    };
                    string json = System.Text.Json.JsonSerializer.Serialize(utilizator);
                    File.WriteAllText(@"C:\Users\cioba\Desktop\Proiect IP\ProiectIP\utilizatori.json", json);
                }
                else
                {
                    // Dacă fișierul există deja, încărcăm datele vechi
                    string jsonVechi = File.ReadAllText(@"C:\Users\cioba\Desktop\Proiect IP\ProiectIP\utilizatori.json");
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
                    File.WriteAllText(@"C:\Users\cioba\Desktop\Proiect IP\ProiectIP\utilizatori.json", jsonNou);
                }

                MessageBox.Show("Contul a fost creat cu succes!");
            }
        }

    }
}
