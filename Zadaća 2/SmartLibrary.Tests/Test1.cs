using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Services;
using SmartLibrary.Data;
using SmartLibrary.Models;
using System.Linq;

namespace SmartLibrary.Tests
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void RegistracijaDodajeKorisnika()
        {
            var repo = new UserRepository();
            var service = new UserService(repo);

            service.RegistrujKorisnika("Test", "User", "t@t.com", "pass123", UserRole.Clan);

            var lista = repo.GetAll().ToList();

            Assert.AreEqual(1, lista.Count);
            Assert.AreEqual("Test", lista[0].Ime);
        }
    }
}
