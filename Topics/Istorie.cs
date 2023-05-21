using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    class Istorie : Topics
    {
        public Istorie()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariIstorie.json"));
            indexIntrebareCurenta = 0;
        }
    }
}
