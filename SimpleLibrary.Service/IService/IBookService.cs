using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IBookService
    {
        void Save(Book book);
        List<Book> GetAllBook();
    }
}
