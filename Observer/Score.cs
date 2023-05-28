using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    public class Score
    {
        private int _scor;
        private int _timp;
        private string _nume;

        private List<IScoreObserver> _observers = new List<IScoreObserver>();
        public void RegisterObserver(IScoreObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers(int score, int timp, string nume, string caleHighScore)
        {
            foreach (var observer in _observers)
            {
                observer.UpdateScore(score, timp, nume, caleHighScore);
            }
        }


        public int scor { get => _scor; set => _scor = value; }
        public int timp { get => _timp; set => _timp = value; }
        public string nume { get => _nume; set => _nume = value; }
    }
}
