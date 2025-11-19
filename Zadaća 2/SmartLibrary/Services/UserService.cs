using SmartLibrary.Data;
using SmartLibrary.Models;
using System;

namespace SmartLibrary.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public void RegistrujKorisnika(string ime, string prezime, string email, string lozinka, UserRole uloga)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Email nije validan!");
            if (string.IsNullOrWhiteSpace(lozinka) || lozinka.Length < 5)
                throw new ArgumentException("Lozinka mora imati barem 5 karaktera!");

            var user = new User
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Lozinka = lozinka,
                Uloga = uloga
            };

            _repo.Add(user);
        }

        public void AzurirajKorisnika(User korisnik) => _repo.Update(korisnik);

        public void ObrisiKorisnika(int id)
        {
            if (!_repo.Remove(id))
                throw new Exception("Korisnik nije pronađen!");
        }

        public void PrikaziSve()
        {
            foreach (var k in _repo.GetAll())
                Console.WriteLine(k);
        }
    }
}
