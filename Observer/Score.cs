/*************************************************************************
* *
* File: Score.cs *
* Descriere: *
* Clasa Score reprezintă un scor. Aceasta conține variabilele private _scor, _timp și _nume, care sunt utilizate pentru*
* a stoca informații despre scorul, timpul și numele asociate jocului.

Clasa are și o listă privată de observatori _observers, care sunt obiecte ce vor fi notificate atunci când scorul este actualizat.*
Acești observatori sunt de tipul IScoreObserver, o interfață care definește metoda*
UpdateScore utilizată pentru a actualiza observatorii cu privire la modificările scorului..*
s
* Autor: Sebastian Miron*
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    /// <summary>
    /// Clasa Score reprezintă un scor în joc. 
    /// Include metode pentru înregistrarea observatorilor și notificarea acestora atunci când scorul este actualizat.
    /// </summary>
    public class Score
    {
        private int _scor;
        private int _timp;
        private string _nume;

        // Lista de observatori care ascultă pentru actualizările scorului
        private List<IScoreObserver> _observers = new List<IScoreObserver>();

        /// <summary>
        /// Metoda RegisterObserver este utilizată pentru a adăuga un nou observator la lista de observatori.
        /// </summary>
        /// <param name="observer">Observatorul care trebuie adăugat la listă.</param>
        public void RegisterObserver(IScoreObserver observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Metoda NotifyObservers este utilizată pentru a notifica toți observatorii cu privire la o actualizare a scorului.
        /// </summary>
        /// <param name="score">Noul scor.</param>
        /// <param name="timp">Timpul în care s-a obținut scorul.</param>
        /// <param name="nume">Numele jucătorului.</param>
        /// <param name="caleHighScore">Calea către fișierul unde sunt stocate scorurile înalte.</param>
        public void NotifyObservers(int score, int timp, string nume, string caleHighScore)
        {
            foreach (var observer in _observers)
            {
                observer.UpdateScore(score, timp, nume, caleHighScore);
            }
        }

        // Proprietățile pentru scor, timp și nume
        public int scor { get => _scor; set => _scor = value; }
        public int timp { get => _timp; set => _timp = value; }
        public string nume { get => _nume; set => _nume = value; }
    }
}
