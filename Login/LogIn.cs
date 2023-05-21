using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JocQuiz
{
    class LogIn
    {
        private string _email;
        private string _parola;
        private string _numeFisier = @"../../utilizatori.json";

        public LogIn(string email, string parola)
        {
            _email = email;
            _parola = parola;
        }

        public bool AccountExists()
        {
            
            // Verificăm dacă fișierul utilizatori.json există
            if (!File.Exists(_numeFisier))
            {
                MessageBox.Show("Fișierul utilizatori.json nu există.", "Eroare");
                return false;
            }

            // Încărcăm conținutul fișierului utilizatori.json
            string json = File.ReadAllText(_numeFisier);
            Console.WriteLine(json);

            // Deserializăm lista de utilizatori din fișierul JSON
            List<Utilizator> utilizatori = System.Text.Json.JsonSerializer.Deserialize<List<Utilizator>>(json);

            // Verificăm dacă există un utilizator cu email și parola introduse
            bool utilizatorExistent = utilizatori.Any(u => u.Email == _email && u.Parola == _parola);

            return utilizatorExistent;
        }

    }
}
