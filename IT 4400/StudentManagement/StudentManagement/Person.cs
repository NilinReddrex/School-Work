using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public abstract class Person
    {
        private readonly ObservableCollection<Course> courses = new ObservableCollection<Course>();
        private string firstName;
        private string lastName;
        private int studentID;
      
        private string gender;
        private int age;
       

        public Person(string firstName, string lastName, int studentID, string gender, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.StudentID = studentID;
            this.Gender = gender;
            this.Age = age;
            
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        public int StudentID
        {
            get
            {
                return studentID;
            }
            set
            {
                studentID = value;
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public abstract string Level { get; }

        public ObservableCollection<Course> Courses
        {
            get
            {
                return courses;
            }
        }


        protected abstract bool CanTakeCourse(Course course);

        public bool TryAddCourse(Course course)
        {
            if (this.CanTakeCourse(course))
            {
                this.courses.Add(course);
                return true;
            }

            return false;
        }

        public void RemoveCourse(Course course)
        {
            this.courses.Remove(course);
        }

        public double GetTotalGPA()
        {
            double GPA = 0;
            double CH = 0;
            foreach (Course course in Courses)
            {
                GPA += (course.GPA * course.CreditHours);
                CH += course.CreditHours;
            }

            double total = GPA / CH;
            return total;
        }
    }

}
