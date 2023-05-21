using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    interface IScoreObserver
    {
        void UpdateScore(int score, int timp, string numeJucator, string caleHighScore);
    }
}
