using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    class Muzica : Topics
    {

        public Muzica()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariMuzica.json"));
            indexIntrebareCurenta = 0;
        }

    }
}
