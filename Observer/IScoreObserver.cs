/*************************************************************************
* *
* File: IScoreObserver.cs *
* Descriere: Interfata observerului.*
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
    /// Interfața IScoreObserver definește o metodă de actualizare a scorului.
    /// Aceasta este folosită pentru a implementa patternul Observer.
    /// </summary>
    public interface IScoreObserver
    {
        /// <summary>
        /// Metoda UpdateScore este folosită pentru a actualiza scorul unui jucător.
        /// Aceasta este chemată atunci când este înregistrat un nou scor.
        /// </summary>
        /// <param name="score">Scorul obținut de jucător.</param>
        /// <param name="timp">Timpul scurs în joc.</param>
        /// <param name="numeJucator">Numele jucătorului.</param>
        /// <param name="caleHighScore">Calea către fișierul unde sunt stocate scorurile înalte.</param>
        void UpdateScore(int score, int timp, string numeJucator, string caleHighScore);
    }
}

