using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class Course
    {
        // private fields for course class
        private string courseName;
        private int courseNumber;
        private double creditHours;
        private double gpa;

        public Course(string courseName, int courseNumber, double creditHours, double gpa)
        {
            this.CourseName = courseName;
            this.CourseNumber = courseNumber;
            this.CreditHours = creditHours;
            this.GPA = gpa;
        }
        // Public properties for course class
        public string CourseName
        {
           get
            {
                return courseName;
            }
            set
            {
                courseName = value;
            }
        }
        public int CourseNumber
        {
            get
            {
                return courseNumber;
            }
            set
            {
                this.courseNumber = value;
            }
        }
        public double CreditHours
        {
            get
            {
                return creditHours;
            }
            set
            {
                creditHours = value;
            }
        }
        public double GPA
        {
            get
            {
                return gpa;
            }
            set
            {
                gpa = value;
            }
        }

    }
}
