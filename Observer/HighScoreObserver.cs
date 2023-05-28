/*************************************************************************
* *
* File: HighScoreObserver.cs *
* Descriere: Implementarea observerului, cu metoda update care adauga in fisierul json, transmis ca parametru, scorul.*
* Autor: Sebastian Miron*
*************************************************************************/


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JocQuiz
{
    /// <summary>
    /// Clasa HighScoreObserver implementează interfața IScoreObserver.
    /// Este folosită pentru a actualiza scorurile în cazul în care s-a realizat un scor nou înalt.
    /// </summary>
    public class HighScoreObserver : IScoreObserver
    {
        /// <summary>
        /// Metoda UpdateScore este responsabilă pentru actualizarea scorurilor.
        /// Dacă fișierul nu există, acesta este creat. Dacă fișierul există deja, noul scor este adăugat la listă.
        /// </summary>
        /// <param name="punctaj">Punctajul jucătorului.</param>
        /// <param name="timpScurs">Timpul scurs în joc.</param>
        /// <param name="numeJucator">Numele jucătorului.</param>
        /// <param name="numeFisier">Numele fișierului în care se salvează scorurile.</param>
        public void UpdateScore(int punctaj, int timpScurs, string numeJucator, string numeFisier)
        {
            // Verificăm dacă fișierul cu scoruri există deja
            if (!File.Exists(numeFisier))
            {
                // Dacă nu există, creăm un nou fișier JSON cu obiectul Score serializat
                Score scor = new Score()
                {
                    scor = punctaj,
                    timp = timpScurs,
                    nume = numeJucator
                };
                string json = JsonConvert.SerializeObject(scor);
                File.WriteAllText(numeFisier, json);
            }
            else
            {
                // Dacă fișierul există deja, încărcăm datele vechi
                string jsonVechi = File.ReadAllText(numeFisier);
                List<Score> scoruri = JsonConvert.DeserializeObject<List<Score>>(jsonVechi);

                // Adăugăm noul scor la lista de scoruri
                scoruri.Add(new Score
                {
                    scor = punctaj,
                    timp = timpScurs,
                    nume = numeJucator
                });

                // Serializăm lista actualizată și rescriem fișierul JSON
                string jsonNou = JsonConvert.SerializeObject(scoruri, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(numeFisier, jsonNou);
            }
        }
    }
}
