using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagementApp.Models;
using StudentManagementApp.Repository;

namespace StudentManagementApp.Services
{
        public class StudentService
        {
            private readonly IStudentRepository _repository;

            public StudentService(IStudentRepository repository)
            {
                _repository = repository;
            }

            public void AddStudent(Student student)
            {
                if (student.Grade < 0 || student.Grade > 100)
                    throw new Exception("Grade must be between 0 and 100");

                _repository.Add(student);
            }

            public List<Student> GetAllStudents()
            {
                return _repository.GetAll();
            }

            public Student GetStudent(int id)
            {
                return _repository.GetById(id);
            }

            public void UpdateStudent(Student student)
            {
                _repository.Update(student);
            }

            public void DeleteStudent(int id)
            {

                _repository.Delete(id);
            }
        }
    }
