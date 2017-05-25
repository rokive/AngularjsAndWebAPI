using IService;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Service
{
    public class BookService :IBookService,ICommon
    {
        IGenericRepository<Book> _repoBook = new GenericRepository<Book>();
        public void Save(Book book)
        {
            _repoBook.Insert(book);
        }

        public List<Book> GetAllBook()
        {
            return _repoBook.GetAllActive().ToList();
        }

        public void SaveChange()
        {
            _repoBook.Save();
        }

        public void RollBack()
        {
            _repoBook.RollBack();
        }
    }
}
