using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    /// <summary>
    /// Clasa Muzica reprezintă o clasă specializată pentru domeniul de muzica.
    /// Aceasta extinde clasa de bază Topics.
    /// </summary>
    class Muzica : Topics
    {
        /// <summary>
        /// Constructorul clasei Muzica.
        /// Inițializează lista de întrebări adăugând întrebările încărcate din fișierul JSON specific domeniului de muzica.
        /// De asemenea, setează indexul întrebării curente la 0.
        /// </summary>
        public Muzica()
        {
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariMuzica.json"));
            indexIntrebareCurenta = 0;
        }

    }
}
