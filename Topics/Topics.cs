using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    /// <summary>
    /// Clasa Topics reprezintă o clasă pentru gestionarea întrebărilor dintr-un anumit domeniu.
    /// </summary>
    class Topics
    {
        private List<Intrebare> _intrebariLista = new List<Intrebare>(20);
        private int _index = 0;

        /// <summary>
        /// Proprietatea intrebari reprezintă lista de întrebări pentru domeniul respectiv.
        /// </summary>
        public List<Intrebare> intrebari
        {
            get => _intrebariLista;
            set => _intrebariLista = value;
        }

        /// <summary>
        /// Proprietatea indexIntrebareCurenta reprezintă indexul întrebării curente în listă.
        /// </summary>
        public int indexIntrebareCurenta
        {
            get => _index;
            set => _index = value;
        }

        /// <summary>
        /// Metoda IncarcaIntrebariDinJson încarcă întrebările dintr-un fișier JSON și le returnează sub formă de listă.
        /// </summary>
        /// <param name="pathToJsonFile">Calea către fișierul JSON care conține întrebările.</param>
        /// <returns>Lista de întrebări încărcate din fișierul JSON.</returns>
        public List<Intrebare> IncarcaIntrebariDinJson(string pathToJsonFile)
        {
            string json = File.ReadAllText(pathToJsonFile);
            List<Intrebare> intrebari = JsonConvert.DeserializeObject<List<Intrebare>>(json);
            return intrebari;
        }
    }
}
