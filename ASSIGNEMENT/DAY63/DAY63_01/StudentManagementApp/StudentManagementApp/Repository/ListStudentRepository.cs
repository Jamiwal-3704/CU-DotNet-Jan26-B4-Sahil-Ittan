using StudentManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementApp.Repository
{
    public class ListStudentRepository : IStudentRepository
    {
        private List<Student> students = new List<Student>();

        public void Add(Student student)
        {
            students.Add(student);
        }

        public List<Student> GetAll()
        {
            return students;
        }

        public Student? GetById(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }

        public void Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Grade = student.Grade;
            }
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
                students.Remove(student);
        }
    }
}
