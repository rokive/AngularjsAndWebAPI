using Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleLibrary.Web.Controllers
{
    public class BookIssueController : ApiController
    {
        public string BookIssueSave(BookIssueMain model)
        {
            BookIssueService _bookService = new BookIssueService();
            StudentService _studentService = new StudentService();
            string msg = "";
            try
            {

                Student studentModel = _studentService.GetStudentById(model.StudentId);
                if (studentModel != null)
                {
                    if (studentModel.TakenBook + model.BookIssueDetails.Count < 5)
                    {
                        studentModel.TakenBook += model.BookIssueDetails.Count;

                        _bookService.Save(model);
                        _studentService.Update(studentModel);

                        _bookService.SaveChange();
                        _studentService.SaveChange();
                        msg = "Save successfully";
                    }
                    else
                    {
                        msg = "Issue excess book";
                    }
                }

            }
            catch (Exception)
            {
                _bookService.RollBack();
                _studentService.RollBack();
                msg = "Save not successfully";
            }
            return msg;
        }
    }
}
