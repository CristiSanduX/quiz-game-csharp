using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    class Sport : Topics
    {
        public Sport()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariSport.json"));
            indexIntrebareCurenta = 0;
        }
    }
}
