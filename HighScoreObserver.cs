using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocQuiz
{
    public class HighScoreObserver : IScoreObserver
    {
        public void UpdateScore(int punctaj, int timpScurs, string numeJucator, string numeFisier)
        {
            if (!File.Exists(numeFisier))
            {
                Score scor = new Score()
                {
                    scor = punctaj,
                    timp = timpScurs,
                    nume = numeJucator
                };
                string json = System.Text.Json.JsonSerializer.Serialize(scor);
                File.WriteAllText(numeFisier, json);
            }
            else
            {
                string jsonVechi = File.ReadAllText(numeFisier);
                List<Score> scoruri = System.Text.Json.JsonSerializer.Deserialize<List<Score>>(jsonVechi);

                scoruri.Add(new Score
                {
                    scor = punctaj,
                    timp = timpScurs,
                    nume = numeJucator
                });

                string jsonNou = JsonConvert.SerializeObject(scoruri, Formatting.Indented);
                File.WriteAllText(numeFisier, jsonNou);
            }
        }
    }
}
