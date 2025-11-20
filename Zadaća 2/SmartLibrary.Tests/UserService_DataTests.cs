using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Services;
using SmartLibrary.Data;
using SmartLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartLibrary.Tests
{
    [TestClass]
    public class UserService_DataTests
    {
        public static IEnumerable<object[]> UserData =>
            new List<object[]>
            {
                new object[] { "Ana", "Anić", "ana@example.com" },
                new object[] { "Marko", "Markić", "marko@example.com" },
                new object[] { "Iva", "Ivić", "iva@example.com" }
            };

        [TestMethod]
        [DynamicData(nameof(UserData))]
        public void Registracija_DataDriven(string ime, string prezime, string email)
        {
            var repo = new UserRepository();
            var service = new UserService(repo);

            service.RegistrujKorisnika(ime, prezime, email, "pass123", UserRole.Clan);

            var lista = repo.GetAll().ToList();

            Assert.AreEqual(1, lista.Count);
            Assert.AreEqual(ime, lista[0].Ime);
            Assert.AreEqual(prezime, lista[0].Prezime);
        }
    }
}
