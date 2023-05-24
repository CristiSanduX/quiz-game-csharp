using JocQuiz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
    }
}
