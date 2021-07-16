using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GradeBookApi.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
       
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Course { get; set; }
        
    }

    public class Exam
    {
        public int ID { get; set; }

        [ForeignKey("student")]
        public int studentID { get; set; }
        public Student student { get; set; }

        [ForeignKey("subject")]
        public int subjectID { get; set; }
        public Subject subject { get; set; }

        public float Marks { get; set; }
        public ClassDay ClassDay { get; set; }

    }

    public enum ClassDay
    {
        Monday=1,
        Tuesday=2,
        Wednesday=3,
        Thursday=4,
        Friday=5,
        Saturday=6,
        Sunday=7,

    }

    public class GradeBookContext : DbContext
    {
        public DbSet<Student> student { get; set; }
        public DbSet<Subject> subject { get; set; }
        public DbSet<Exam> exam { get; set; }
        
    }

}