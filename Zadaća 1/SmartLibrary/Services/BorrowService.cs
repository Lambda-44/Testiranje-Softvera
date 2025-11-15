using System;
using SmartLibrary.Models;
using SmartLibrary.Data;
using SmartLibrary.Inventory;

namespace SmartLibrary.Services
{
    public class BorrowService
    {
        private const int LoanPeriodDays = 7;     // Rok vraćanja koji smo postavili, s obzirom da nam nije dat tamo u postavci
        private const double FinePerDay = 1.0;     // Kazna u KM po danu, mozemo da stavimo proizvljno, stavili smo marka po danu.

        private readonly BorrowRepository _repo;
        private readonly LibraryInventory _inventory;
        private readonly UserRepository _users;

        public BorrowService(BorrowRepository repo, LibraryInventory inventory, UserRepository users)
        {
            _repo = repo;
            _inventory = inventory;
            _users = users;
        }

        public void Posudi(int userId, int bookId)
        {
            var user = _users.GetById(userId);
            if (user == null) throw new Exception("Korisnik ne postoji.");

            var book = _inventory.GetAllBooks().Find(b => b.Id == bookId);
            if (book == null) throw new Exception("Knjiga ne postoji.");
            if (!book.IsAvailable) throw new Exception("Knjiga nije dostupna.");

            var record = new BorrowRecord
            {
                UserId = userId,
                BookId = bookId,
                DateBorrowed = DateTime.Now,
                DueDate = DateTime.Now.AddDays(LoanPeriodDays)
            };

            _repo.Add(record);
            _inventory.SetAvailability(bookId, false);
        }

        public void Vrati(int borrowId)
        {
            var record = _repo.GetById(borrowId);
            if (record == null) throw new Exception("Posudba ne postoji.");
            if (record.IsReturned) throw new Exception("Knjiga je već vraćena.");

            record.ReturnedDate = DateTime.Now;

            if (record.ReturnedDate.Value > record.DueDate)
            {
                int daysLate = (record.ReturnedDate.Value - record.DueDate).Days;
                record.Fine = daysLate * FinePerDay;
            }

            _inventory.SetAvailability(record.BookId, true);
        }

        public void PrikaziAktivne()
        {
            foreach (var r in _repo.GetActive())
                Console.WriteLine(r);
        }

        public void PrikaziHistoriju()
        {
            foreach (var r in _repo.GetHistory())
                Console.WriteLine(r);
        }
    }
}
