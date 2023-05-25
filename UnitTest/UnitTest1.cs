using JocQuiz;
using Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AccountExistsVerif()
        {
            string email = "diana@yahoo.com";
            string password = "diana123";
            LogIn login = new LogIn(email, password);
            Assert.IsTrue(login.AccountExists());

        }

        [TestMethod]
        public void AccountNotExistsVerif()
        {
            string email = "inexistent@yahoo.com";
            string password = "inexistent123";
            LogIn login = new LogIn(email, password);
            Assert.IsFalse(login.AccountExists());
        }

        [TestMethod]
        public void VerificareEmail()
        {
            string email = "test";
            string password = "test";
            string name = "Test";
            Assert.ThrowsException<Exception>(() => new SignUp(email, name, password), "Adresa de e-mail introdusă nu este validă.");
        }

        [TestMethod]
        public void VerificareParola()
        {
            string email = "Test@yahoo.ro";
            string password = "test";
            string name = "Test";
            Assert.ThrowsException<Exception>(() => new SignUp(email, name, password), "Parola trebuie să aibă cel puțin 8 caractere.");

        }

        [TestMethod]
        public void VerificareExistaNume()
        {
            string email = "test@yahoo.ro";
            string password = "test123";
            Assert.ThrowsException<Exception>(() => new SignUp(email, null, password), "Adresa de e-mail introdusă nu este validă.");

        }

        [TestMethod]
        public void VerificareAccountExistent()
        {
            Assert.ThrowsException<Exception>(() => (new SignUp("sebi147852@yahoo.ro", "sebi1234", "Sebastian")).CreateAccount(), "Exista deja un utilizator cu acest email.");
        }

        [TestMethod]
        [Ignore]
        public void VerificareCreateAccount()
        {
            string json = File.ReadAllText(@"../../utilizatori.json");
            List<Utilizator> utilizatori = JsonConvert.DeserializeObject<List<Utilizator>>(json);
            int counter = utilizatori.Count;
            Random x = new Random();
            string email = "abc" + x.Next() + "@yahoo.com";
            (new SignUp(email, "sebi1234", "Sebastian")).CreateAccount();
            utilizatori.Clear();
            json = File.ReadAllText(@"../../utilizatori.json");
            utilizatori = JsonConvert.DeserializeObject<List<Utilizator>>(json);
            Assert.AreEqual(counter + 1, utilizatori.Count);
        }

        [TestMethod]
        public void VerificareFisierNume()
        {
            Assert.IsTrue(File.Exists(@"../../utilizatori.json"));
        }

        [TestMethod]
        public void VerificareExistaEmail()
        {
            string password = "test123";
            string name = "Test";
            Assert.ThrowsException<Exception>(() => new SignUp(null, name, password), "Vă rugăm să completați toate câmpurile.");
        }

        [TestMethod]
        public void VerificareExistaParola()
        {
            string email = "test@yahoo.ro";
            string name = "Test";
            Assert.ThrowsException<Exception>(() => new SignUp(email, name, null), "Vă rugăm să completați toate câmpurile.");
        }

        [TestMethod]
        public void VerificareConstructorLogIn()
        {
            string email = "test@yahoo.com";
            string password = "test123";
            LogIn login = new LogIn(email, password);

            FieldInfo emailField = typeof(LogIn).GetField("_email", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo passwordField = typeof(LogIn).GetField("_parola", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.AreEqual(email, emailField.GetValue(login));
            Assert.AreEqual(password, passwordField.GetValue(login));
        }

        [TestMethod]
        public void VerificareConstructorSignUp()
        {
            string email = "test@yahoo.com";
            string password = "test1234";
            string name = "Test";
            SignUp signUp = new SignUp(email, name, password);

            FieldInfo emailField = typeof(SignUp).GetField("_email", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo passwordField = typeof(SignUp).GetField("_parola", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo nameField = typeof(SignUp).GetField("_nume", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.AreEqual(email, emailField.GetValue(signUp));
            Assert.AreEqual(password, passwordField.GetValue(signUp));
            Assert.AreEqual(name, nameField.GetValue(signUp));
        }

        [TestMethod]
        public void VerificareIntrebare()
        {
            Intrebare intrebare = new Intrebare
            {
                intrebare = "Care este capitala Angliei?",
                variante = new List<string> { "Londra", "Liverpool", "Rusia", "Manchester" },
                raspuns = "Londra"
            };

            Assert.AreEqual("Care este capitala Angliei?", intrebare.intrebare);
            Assert.AreEqual(4, intrebare.variante.Count);
            Assert.AreEqual("Londra", intrebare.raspuns);
        }

        [TestMethod]
        public void VerificareScoreInitializare()
        {
            Score score = new Score();
            score.scor = 10;
            score.timp = 20;
            score.nume = "test";

            Assert.AreEqual(10, score.scor);
            Assert.AreEqual(20, score.timp);
            Assert.AreEqual("test", score.nume);
        }

        /// <summary>
        /// Acest test verifică funcționarea metodei RegisterObserver din clasa Score.
        /// În primul rând, se creează un obiect Score și un obiect HighScoreObserver.
        /// Apoi, observerul este înregistrat folosind metoda RegisterObserver.
        /// Testul extrage lista internă de observatori din obiectul Score și verifică dacă acesta conține observerul înregistrat.
        /// </summary>
        [TestMethod]
        public void VerificareScoreRegisterObserver()
        {
            Score score = new Score();
            HighScoreObserver observer = new HighScoreObserver();

            score.RegisterObserver(observer);

            FieldInfo observersField = typeof(Score).GetField("_observers", BindingFlags.NonPublic | BindingFlags.Instance);

            List<IScoreObserver> observers = (List<IScoreObserver>)observersField.GetValue(score);

            Assert.IsTrue(observers.Contains(observer));
        }

        /// <summary>
        /// Acest test verifică dacă metoda NotifyObservers din clasa Score funcționează corect.
        /// În primul rând, se creează un mock pentru interfața IScoreObserver și se înregistrează acest mock ca un observer în obiectul Score.
        /// Apoi, se apelează metoda NotifyObservers cu anumite valori.
        /// În final, se verifică dacă metoda UpdateScore a observerului mock a fost apelată exact o dată cu aceleași valori cu care a fost apelată metoda NotifyObservers.
        /// </summary>
        [TestMethod]
        public void VerificareScoreNotifyObservers()
        {
            var mockObserver = new Mock<IScoreObserver>();
            Score score = new Score();
            score.RegisterObserver(mockObserver.Object);

            int testScor = 5;
            int testTimp = 10;
            string testNume = "Test";
            string testCaleHighScore = "testHighScore";

            score.NotifyObservers(testScor, testTimp, testNume, testCaleHighScore);

            mockObserver.Verify(o => o.UpdateScore(testScor, testTimp, testNume, testCaleHighScore), Times.Once());
        }

    }
}
