using SmartLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartLibrary.Data
{
    public class UserRepository
    {
        private List<User> _korisnici = new();
        private int _nextId = 1;

        public IEnumerable<User> GetAll() => _korisnici;

        public User? GetById(int id) => _korisnici.FirstOrDefault(u => u.Id == id);

        public void Add(User korisnik)
        {
            korisnik.Id = _nextId++;
            _korisnici.Add(korisnik);
        }

        public bool Remove(int id)
        {
            var user = GetById(id);
            if (user == null) return false;
            _korisnici.Remove(user);
            return true;
        }

        public bool Update(User updatedUser)
        {
            var existing = GetById(updatedUser.Id);
            if (existing == null) return false;

            existing.Ime = updatedUser.Ime;
            existing.Prezime = updatedUser.Prezime;
            existing.Email = updatedUser.Email;
            existing.Lozinka = updatedUser.Lozinka;
            existing.Uloga = updatedUser.Uloga;
            return true;
        }
    }
}
