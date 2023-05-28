using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    /// <summary>
    /// Clasa Sport reprezintă o clasă specializată pentru domeniul de sport.
    /// Aceasta extinde clasa de bază Topics.
    /// </summary>
    class Sport : Topics
    {
        /// <summary>
        /// Constructorul clasei Sport.
        /// Inițializează lista de întrebări adăugând întrebările încărcate din fișierul JSON specific domeniului de sport.
        /// De asemenea, setează indexul întrebării curente la 0.
        /// </summary>
        public Sport()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariSport.json"));
            indexIntrebareCurenta = 0;
        }
    }
}
