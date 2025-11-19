using SmartLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartLibrary.Data
{
    public class BorrowRepository
    {
        private List<BorrowRecord> _records = new();
        private int _nextId = 1;

        public void Add(BorrowRecord record)
        {
            record.Id = _nextId++;
            _records.Add(record);
        }

        public BorrowRecord? GetById(int id) =>
            _records.FirstOrDefault(r => r.Id == id);

        public IEnumerable<BorrowRecord> GetActive() =>
            _records.Where(r => !r.IsReturned);

        public IEnumerable<BorrowRecord> GetHistory() =>
            _records;

        public IEnumerable<BorrowRecord> GetByUser(int userId) =>
            _records.Where(r => r.UserId == userId);
    }
}
