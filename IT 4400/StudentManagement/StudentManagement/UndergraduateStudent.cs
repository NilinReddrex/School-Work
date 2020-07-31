using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class UndergraduateStudent : Person
    {
        public UndergraduateStudent(string firstName, string lastName, int studentID, string gender, int age) : base(firstName, lastName, studentID, gender, age)
        {
        }

        public override string Level => "Undergraduate";

        protected override bool CanTakeCourse(Course course)
        {
            if (course.CourseNumber >= 1000 && course.CourseNumber <= 4999)
            {
                return true;
            }

            return false;
        }
    }
}
