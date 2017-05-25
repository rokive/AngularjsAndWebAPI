using IService;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repositories;
namespace Service
{
    public class StudentService : IStudentService, ISequenceService,ICommon
    {
        IGenericRepository<Student> _repoStudent = new GenericRepository<Student>();
        public void Save(Student student)
        {
            _repoStudent.Insert(student);
        }

        public List<Student> GetAllStudent()
        {
            return _repoStudent.GetAllActive().ToList();
        }

        public Student GetStudentById(int Id)
        {
            return _repoStudent.GetAllActive().Where(m => m.StudentId == Id).FirstOrDefault();
        }

        public void Update(Student student)
        {
            _repoStudent.Update(student);
        }

        public int GetSequence()
        {
            return _repoStudent.GetSequence();
        }

        public void SaveChange()
        {
            _repoStudent.Save();
        }

        public void RollBack()
        {
            _repoStudent.RollBack();
        }
    }
}
