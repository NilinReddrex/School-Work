using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagement
{
    /// <summary>
    /// MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Person> people = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            this.StudentsListBox.ItemsSource = this.People;
        }

        public ObservableCollection<Person> People
        {
            get
            {
                return this.people;
            }
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = this.GetStudentFromForm();
                people.Add(person);
                this.ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            Course course = this.GetCourseFromForm();
            
            Person person = this.StudentsListBox.SelectedItem as Person;
            if (person == null)
            {
                MessageBox.Show("A student must be selected to add a course.");
                return;
            }

            if (!person.TryAddCourse(course))
            {
                MessageBox.Show($"Provided course is not a vaild {person.Level} course.");
                return;
            }

            this.ClearForm();
            TotalGPATextBox.Text = person.GetTotalGPA().ToString();
        }

        private Course GetCourseFromForm()
        {
            string courseName = CourseNameTextBox.Text;
            string courseNumber = CourseNumberTextBox.Text;
            string creditHours = CreditHoursTextBox.Text;
            string gpa = GPATextBox.Text;


            if (this.AnyNullOrWhiteSpace(courseName, courseNumber, creditHours, gpa))
            {
                throw new Exception("All properties are required.");
            }

            if (courseNumber.Length != 4)
            {
                throw new Exception("Course numbers must be four digits long.");
            }

            if (!int.TryParse(courseNumber, out int courseNum))
            {
                throw new Exception("Course number must be a number.");
            }

            if (!double.TryParse(creditHours, out double studentCreditHours))
            {
                throw new Exception("Credit hours must be a number.");
            }

            if (!double.TryParse(gpa, out double studentGPA))
            {
                throw new Exception("Credit hours must be a number.");
            }

            if (studentGPA < 0.0 || studentGPA > 4.0)
            {
                throw new Exception("GPA can only be between 0.0 and 4.0.");
            }

            return new Course(courseName, courseNum, studentCreditHours, studentGPA);
        }

        private bool AnyNullOrWhiteSpace(params string[] inputs)
        {
            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return true;
                }
            }

            return false;
        }

        private void ClearForm()
        {
            foreach (UIElement element in this.MainGrid.Children)
            {
                TextBox textBox = element as TextBox;
                if (textBox != null)
                {
                    textBox.Clear();
                }
            }

            if (this.StudentsListBox.SelectedItem != null)
            {
                this.StudentsListBox_SelectionChanged(this.StudentsListBox.SelectedItem, null);
            }
        }

        private void StudentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person person = this.StudentsListBox.SelectedItem as Person;
            if (person != null)
            {
                FirstNameTextBox.Text = person.FirstName;
                LastNameTextBox.Text = person.LastName;
                StudentIDTextBox.Text = person.StudentID.ToString();
                GenderComboBox.Text = person.Gender;
                AgeTextBox.Text = person.Age.ToString();
                LevelComboBox.Text = person.Level;
                TotalGPATextBox.Text = person.GetTotalGPA().ToString();

                this.CoursessListBox.ItemsSource = person.Courses;
            }
        }

        private void CoursessListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Course courses = this.CoursessListBox.SelectedItem as Course;
            if (courses != null)
            {
                CourseNameTextBox.Text = courses.CourseName;
                CourseNumberTextBox.Text = courses.CourseNumber.ToString();
                GPATextBox.Text = courses.GPA.ToString();
                CreditHoursTextBox.Text = courses.CreditHours.ToString();
            }
        }

        private void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            Person currentStudent = this.StudentsListBox.SelectedItem as Person;
            if (currentStudent != null)
            {
                try
                {
                    Person person = this.GetStudentFromForm();

                    if (currentStudent.Level != person.Level)
                    {
                        if( currentStudent.Courses.Count != 0)
                        {
                            MessageBox.Show("Can not change student level while courses are assigned.");
                            return;
                        }

                        people.Remove(currentStudent);
                        people.Add(person);
                        this.StudentsListBox.SelectedItem = person;
                        return;
                    }

                    currentStudent.FirstName = person.FirstName;
                    currentStudent.LastName = person.LastName;
                    currentStudent.StudentID = person.StudentID;
                    currentStudent.Gender = person.Gender;
                    currentStudent.Age = person.Age;
                    this.StudentsListBox.Items.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            Person person = this.StudentsListBox.SelectedItem as Person;
            if (person != null)
            {
                people.Remove(person);
                this.CoursessListBox.ItemsSource = null;
                ClearForm();
            }
        }

        private void UpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            Course currentCourse = this.CoursessListBox.SelectedItem as Course;
            if (currentCourse != null)
            {
                try
                {
                    Course course = this.GetCourseFromForm();

                    currentCourse.CourseName = course.CourseName;
                    currentCourse.CourseNumber = course.CourseNumber;
                    currentCourse.CreditHours = course.CreditHours;
                    currentCourse.GPA = course.GPA;

                    this.CoursessListBox.Items.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            Person person = this.StudentsListBox.SelectedItem as Person;
            if (person != null)
            {
                Course course = this.CoursessListBox.SelectedItem as Course;
                if (course != null)
                {
                    person.Courses.Remove(course);
                    ClearForm();
                }
            }
        }

        private Person GetStudentFromForm()
        {
            string fname = FirstNameTextBox.Text;
            string id = StudentIDTextBox.Text;
            string lname = LastNameTextBox.Text;
            string age = AgeTextBox.Text;
            string gender = GenderComboBox.Text;
            string level = LevelComboBox.Text;

            if (this.AnyNullOrWhiteSpace(fname, id, lname, age, gender, level))
            {
                throw new Exception("All student properties are required.");
            }

            if (!int.TryParse(id, out int studentId))
            {
                throw new Exception("Student ID must be a number.");
            }

            if (!int.TryParse(age, out int studentAge))
            {
                throw new Exception("Student age must be a number.");
            }

            if (level == "Undergraduate")
            {
                return new UndergraduateStudent(fname, lname, studentId, gender, studentAge);
            }
            else
            {
                return new GraduateStudent(fname, lname, studentId, gender, studentAge);
            }
        }
    }
}
