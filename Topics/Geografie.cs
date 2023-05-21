using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    class Geografie : Topics
    {
        public Geografie()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariGeografie.json"));
            indexIntrebareCurenta = 0;
        }
    }
}
