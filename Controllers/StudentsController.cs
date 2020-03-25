using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cw4_v3.DAL;
using cw4_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw4_v3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

       [HttpGet]
        public IActionResult GetStudent()
        {
            List<Student> listOfStudents = new List<Student>();
            
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18239;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText ="select * from Student " + "inner join Enrollment on Student.IdEnrollment = Enrollment.IdEnrollment " + "inner join Studies on Studies.IdStudy = Enrollment.IdStudy";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    st.NameOfStudies = dr["Name"].ToString();
                    listOfStudents.Add(st);
                }

            }

            return Ok(listOfStudents);
        }

        [HttpGet("{id}")]
        public IActionResult GetStEnroll(string id)
        {
            
            List<Enrollment> listOfEnrollments = new List<Enrollment>();
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18239;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Enrollment inner join Student on Enrollment.IdEnrollment = Student.IdEnrollment where Student.IndexNumber = @id";
                com.Parameters.AddWithValue("id", id);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Enrollment enrollment = new Enrollment();
                    enrollment.IdEnrollment = dr["IdEnrollment"].ToString();
                    enrollment.Semester = dr["Semester"].ToString();
                    enrollment.IdStudy = dr["IdStudy"].ToString();
                    enrollment.StartDate = dr["StartDate"].ToString();
                    listOfEnrollments.Add(enrollment);
                }

                return Ok(listOfEnrollments);
            }

        }
        /*
        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            var service = new MockDbService();
            return Ok(service.GetStudents());
        }

        [HttpGet("{id}")]

        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            }
            else if (id == 2)
            {
                return Ok("Malewski");
            }
            return NotFound("Nie znaleziono studenta");
        }
        */

        [HttpDelete]
        public IActionResult DeleteStudent(string id)
        {
            //delete 
            return Ok($"Usuwanie dokończone {id}");
        }

        [HttpPut]
        public IActionResult PutStudent(string id)
        {
            //change
            return Ok($"Aktualizacja dokończona {id}");
        }


        [HttpPost]

        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }


    }

}
