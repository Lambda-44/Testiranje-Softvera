using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartLibrary.Inventory
{
    public class LibraryInventory
    {
        private List<Book> books = new List<Book>();
        private int nextId = 1;

        public void AddBook(string title, string author, string genre)
        {
            books.Add(new Book
            {
                Id = nextId++,
                Title = title,
                Author = author,
                Genre = genre,
                IsAvailable = true
            });
        }

        public bool UpdateBook(int id, string newTitle, string newAuthor, string newGenre)
        {
            Book book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            book.Title = newTitle;
            book.Author = newAuthor;
            book.Genre = newGenre;
            return true;
        }

        public bool DeleteBook(int id)
        {
            Book book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            books.Remove(book);
            return true;
        }

        public List<Book> Search(string query)
        {
            query = query.ToLower();
            return books.Where(b =>
                b.Title.ToLower().Contains(query) ||
                b.Author.ToLower().Contains(query) ||
                b.Genre.ToLower().Contains(query)
            ).ToList();
        }

        public bool SetAvailability(int id, bool available)
        {
            Book book = books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            book.IsAvailable = available;
            return true;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }
    }
}
