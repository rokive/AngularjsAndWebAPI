using Models;
using SimpleLibrary.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleLibrary.Web.Controllers
{
    public class BookController : ApiController
    {
        [HttpPost]
        public string BookSave(Book model)
        {
            BookService _service = new BookService();
            string msg = "";
            try
            {
                if (GetAllBook().Where(m=>m.ISBN==model.ISBN).Count()==0)
                {
                    _service.Save(model);
                    _service.SaveChange();
                    msg = "Save successfully";    
                }
                else
                {
                    msg = "This book already exist";
                }
                
            }
            catch (Exception)
            {
                _service.RollBack();
                msg = "Save not successfully";
            }
            return msg;
        }
        [HttpGet]
        public List<Book> GetAllBook()
        {
            BookService _service = new BookService();
            List<Book> bookList = _service.GetAllBook();
            return bookList;
        }
    }
}
