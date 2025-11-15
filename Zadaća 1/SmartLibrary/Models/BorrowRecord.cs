using System;

namespace SmartLibrary.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime? ReturnedDate { get; set; }
        public double Fine { get; set; } = 0;

        public bool IsReturned => ReturnedDate.HasValue;

        public override string ToString()
        {
            string status = IsReturned
                ? $"Vraćena: {ReturnedDate.Value.ToShortDateString()}, Kazna: {Fine} KM"
                : $"Rok: {DueDate.ToShortDateString()}, Kasni: {(DateTime.Now > DueDate ? "DA" : "NE")}";

            return $"[#{Id}] User {UserId} -> Book {BookId} | {status}";
        }
    }
}
