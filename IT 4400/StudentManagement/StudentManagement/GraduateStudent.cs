using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class GraduateStudent : Person
    {
        public GraduateStudent(string firstName, string lastName, int studentID, string gender, int age) : base(firstName, lastName, studentID, gender, age)
        {
        }

        public override string Level => "Graduate";

        protected override bool CanTakeCourse(Course course)
        {
            if (course.CourseNumber >= 5000 && course.CourseNumber <= 9999)
            {
                return true;
            }

            return false;
        }
    }
}
