using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    class Topics
    {
        private List<Intrebare> _intrebariLista = new List<Intrebare>(20);
        private int _index = 0 ;

        public List<Intrebare> intrebari
        {
            get => _intrebariLista;
            set => _intrebariLista = value;
        }

        public int indexIntrebareCurenta
        {
            get => _index;
            set => _index = value;
        }
        public List<Intrebare> IncarcaIntrebariDinJson(string pathToJsonFile)
        {
            string json = File.ReadAllText(pathToJsonFile);
            List<Intrebare> intrebari = JsonConvert.DeserializeObject<List<Intrebare>>(json);
            return intrebari;
        }
    }
}
