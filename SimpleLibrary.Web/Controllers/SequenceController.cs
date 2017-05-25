
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleLibrary.Web.Controllers
{
    public class SequenceController : ApiController
    {
       
        //[HttpGet()]
        //[ActionName("GetSequence")]
        public int GetSequence()
        {
            StudentService _service = new StudentService();
            int id = _service.GetSequence();
            return id;
        }
    }
}
