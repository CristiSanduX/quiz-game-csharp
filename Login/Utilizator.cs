using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    /// <summary>
    /// Clasa Utilizator reprezintă un utilizator în sistem.
    /// Conține informații precum numele, emailul și parola utilizatorului.
    /// </summary>
    public class Utilizator
    {
        /// <summary>
        /// Proprietatea Nume reprezintă numele utilizatorului.
        /// </summary>
        public string Nume { get; set; }

        /// <summary>
        /// Proprietatea Email reprezintă adresa de email a utilizatorului.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Proprietatea Parola reprezintă parola contului utilizatorului.
        /// </summary>
        public string Parola { get; set; }
    }
}
