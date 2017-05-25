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
    public class StudentController : ApiController
    {

        //[HttpPost()]
        //[ActionName("SaveStudent")]
        public string SaveStudent([FromBody]Student model)
        {
            string msg = "";

            StudentService _service = new StudentService();
            try
            {
                _service.Save(model);
                _service.SaveChange();
                msg = "Save successfully";
            }
            catch (Exception)
            {
                _service.RollBack();   
                msg = "Save not successfully";
            }

            return msg;
        }
        ////[HttpGet()]
        ////[ActionName("GetAllStudent")]
        public List<Student> GetAllStudent()
        {
            StudentService _service = new StudentService();
            List<Student> studentList = _service.GetAllStudent();
            return studentList;
        }

    }
}
