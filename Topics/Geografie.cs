using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    /// <summary>
    /// Clasa Geografie reprezintă o clasă specializată pentru domeniul de geografie.
    /// Aceasta extinde clasa de bază Topics.
    /// </summary>
    class Geografie : Topics
    {
        /// <summary>
        /// Constructorul clasei Geografie.
        /// Inițializează lista de întrebări adăugând întrebările încărcate din fișierul JSON specific domeniului de geografie.
        /// De asemenea, setează indexul întrebării curente la 0.
        /// </summary>
        public Geografie()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariGeografie.json"));
            indexIntrebareCurenta = 0;
        }
    }
}
