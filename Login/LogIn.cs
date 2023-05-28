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

        /// <summary>
        /// Constructor pentru clasa LogIn. 
        /// Inițializează emailul și parola pentru instanța curentă.
        /// </summary>
        /// <param name="email">Emailul utilizatorului.</param>
        /// <param name="parola">Parola utilizatorului.</param>
        public LogIn(string email, string parola)
        {
            _email = email;
            _parola = parola;
        }

        /// <summary>
        /// Verifică dacă un cont există în fișierul JSON pe baza emailului și parolei.
        /// </summary>
        /// <returns>Întoarce true dacă există contul, false în caz contrar.</returns>
        public bool AccountExists()
        {
            // Verificăm dacă fișierul utilizatori.json există
            if (!File.Exists(_numeFisier))
            {
                // Aruncăm o excepție dacă fișierul nu există
                throw new Exception("Fișierul utilizatori.json nu există.");
            }

            // Încărcăm conținutul fișierului utilizatori.json
            string json = File.ReadAllText(_numeFisier);

            // Deserializăm lista de utilizatori din fișierul JSON
            List<Utilizator> utilizatori = JsonConvert.DeserializeObject<List<Utilizator>>(json);

            // Verificăm dacă există un utilizator cu email și parola introduse
            bool utilizatorExistent = utilizatori.Any(u => u.Email == _email && u.Parola == _parola);

            // Parcurgem lista de utilizatori și actualizăm numele dacă utilizatorul există
            utilizatori.ForEach(u =>
            {
                if (u.Email == _email && u.Parola == _parola)
                    _nume = u.Nume;
            });

            // Întoarcem true dacă utilizatorul există, false în caz contrar
            return utilizatorExistent;
        }

        /// <summary>
        /// Proprietate pentru obținerea numelui utilizatorului curent.
        /// </summary>
        public string nume { get => _nume; }
    }
}
