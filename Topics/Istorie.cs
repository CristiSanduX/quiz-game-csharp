using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{       
    /// <summary>
    /// Clasa Istorie reprezintă o clasă specializată pentru domeniul de istorie.
    /// Aceasta extinde clasa de bază Topics.
    /// </summary>
    class Istorie : Topics
    {
        /// <summary>
        /// Constructorul clasei Istorie.
        /// Inițializează lista de întrebări adăugând întrebările încărcate din fișierul JSON specific domeniului de istorie.
        /// De asemenea, setează indexul întrebării curente la 0.
        /// </summary>
        public Istorie()
        {
            
            intrebari.AddRange(IncarcaIntrebariDinJson("../../IntrebariQuiz/IntrebariIstorie.json"));
            indexIntrebareCurenta = 0;
        }
    }
}
