using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class LogIn
    {
        private string _email;
        private string _parola;
        private string _numeFisier = @"../../utilizatori.json";
        private string _nume;
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
                throw new Exception("Fișierul utilizatori.json nu există.");
            }

            // Încărcăm conținutul fișierului utilizatori.json
            string json = File.ReadAllText(_numeFisier);

            // Deserializăm lista de utilizatori din fișierul JSON
            List<Utilizator> utilizatori = JsonConvert.DeserializeObject<List<Utilizator>>(json);

            // Verificăm dacă există un utilizator cu email și parola introduse
            bool utilizatorExistent = utilizatori.Any(u => u.Email == _email && u.Parola == _parola);
            utilizatori.ForEach(u =>
            {
                if (u.Email == _email && u.Parola == _parola)
                    _nume = u.Nume;
            });
            return utilizatorExistent;
        }

        public string nume { get => _nume; }
    }
}
