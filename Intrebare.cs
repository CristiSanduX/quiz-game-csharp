using System.Collections.Generic;

namespace JocQuiz
{
    /// <summary>
    /// Clasa Intrebare reprezintă o întrebare de quiz în joc.
    /// </summary>
    public class Intrebare
    {
        /// <summary>
        /// Proprietatea 'intrebare' reprezintă textul întrebării care va fi prezentat jucătorului.
        /// </summary>
        public string intrebare { get; set; }

        /// <summary>
        /// Proprietatea 'variante' reprezintă lista cu variantele de răspuns la întrebare.
        /// Jucătorul trebuie să aleagă un răspuns din această listă.
        /// </summary>
        public List<string> variante { get; set; }

        /// <summary>
        /// Proprietatea 'raspuns' reprezintă răspunsul corect la întrebare.
        /// Răspunsul oferit de jucător va fi comparat cu acesta pentru a determina dacă a răspuns corect.
        /// </summary>
        public string raspuns { get; set; }
    }
}
