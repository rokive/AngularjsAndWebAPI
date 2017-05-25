using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IStudentService
    {
        void Save(Student student);
        List<Student> GetAllStudent();
        Student GetStudentById(int Id);
        void Update(Student student);
    }
}
