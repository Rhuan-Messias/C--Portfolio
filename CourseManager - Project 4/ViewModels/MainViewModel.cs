using Caliburn.Micro;
using CourseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CourseManager.Repository;
using System.Security.Cryptography;
using System.Data;

namespace CourseManager.ViewModels
{
    internal class MainViewModel : Screen
    {
     //This logic converts the model into ViewModels so the Views can use
     //Model <- ViewModel <- View
        private BindableCollection<EnrollmentModel> _enrollments = new BindableCollection<EnrollmentModel>();
        private BindableCollection<StudentModel> _students = new BindableCollection<StudentModel>();
        private BindableCollection<CourseModel> _courses = new BindableCollection<CourseModel>();
        private readonly string _connectionString = @"Data Source=localhost;Initial Catalog=CourseReport;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private string _appStatus;
        private EnrollmentModel _selectedEnrollment;
        private EnrollmentCommand _enrollmentCommand;

        public MainViewModel()
        {
            SelectedEnrollment = new EnrollmentModel(); // The property will be what we click in the ListView

            try
            {
                _enrollmentCommand = new EnrollmentCommand(_connectionString);
                Enrollments.AddRange(_enrollmentCommand.GetList()); // Add a list to another. . . We can bind with x:name="Enrollments" in XAML


                StudentCommand studentCommand = new StudentCommand(_connectionString);
                Students.AddRange(studentCommand.GetList());//AddRange adds a list to another

                CourseCommand courseCommand = new CourseCommand(_connectionString);
                Courses.AddRange(courseCommand.GetList());
            }
            catch (Exception ex)
            {
                AppStatus = ex.Message;
                NotifyOfPropertyChange(() => AppStatus); //This notify the UI of any changes -> This will update the TextBlock with the AppStatus Binding
            }
        }

        //This makes the select grid appears in the box, by connecting them by the Id
        public CourseModel SelectedEnrollmentCourse
        {
            get
            {
                try
                {
                    //When you have an ID, that could be the key for you to find the rest of the information
                    var courseDictionary = _courses.ToDictionary(key => key.CourseId); // This way i turn my _courses list into a dictionary

                    if (SelectedEnrollment != null && courseDictionary.ContainsKey(SelectedEnrollment.CourseId))
                    {
                        return courseDictionary[SelectedEnrollment.CourseId];
                    }
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
                return null;
                
            }
            set
            {
                try
                {
                    var selectedEnrollmentCourse = value;//Whatever the course we're trying to insert

                    SelectedEnrollment.CourseId = selectedEnrollmentCourse.CourseId;

                    NotifyOfPropertyChange(() => SelectedEnrollment); // Telling the user interface that the SelectedEnrollment has Changed
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
            }
        }

        public StudentModel SelectedEnrollmentStudent
        {
            get
            {
                try
                {
                    //When you have an ID, that could be the key for you to find the rest of the information
                    var studentDictionary = _students.ToDictionary(key => key.StudentId); // This way i turn my _courses list into a dictionary

                    if (SelectedEnrollment != null && studentDictionary.ContainsKey(SelectedEnrollment.StudentId))
                    {
                        return studentDictionary[SelectedEnrollment.StudentId];
                    }
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
                return null;

            }
            set
            {
                try
                {
                    var selectedEnrollmentStudent = value;//Whatever the course we're trying to insert

                    SelectedEnrollment.StudentId = selectedEnrollmentStudent.StudentId;

                    NotifyOfPropertyChange(() => SelectedEnrollment); // Telling the user interface that the SelectedEnrollment has Changed
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
            }
        }

        //Property Enrollments -> the BindableCollection allows us to bind in the UI XAML
        public BindableCollection<EnrollmentModel> Enrollments
        {
            get { return _enrollments; }
            set { _enrollments = value; }
        }
        //Property Students -> This is Binded in the Student ComboBox
        public BindableCollection<StudentModel> Students
        {
            get { return _students; }
            set { _students = value; }
        }

        //Property Students -> This is Binded in the Student ComboBox
        public BindableCollection<CourseModel> Courses
        {
            get { return _courses; }
            set { _courses = value; }
        }

        //Property AppStatus
        public string AppStatus
        {
            get { return _appStatus; }
            set { _appStatus = value; }
        }

        //Property SelectedEnrollment
        public EnrollmentModel SelectedEnrollment
        {
            get
            {
                return _selectedEnrollment; //when asked what the var is
            }
            set
            {
                _selectedEnrollment = value; // when setting a new value, this is how we assign a value to property
                NotifyOfPropertyChange(() => SelectedEnrollment); //If we change this one, and it define the SelectedEnrollment Course
                NotifyOfPropertyChange(() => SelectedEnrollmentCourse);// We need to also notify this one because of that
                NotifyOfPropertyChange(() => SelectedEnrollmentStudent);
            }
        }

        public void CreateNewEnrollment()
        {
            try
            {
                SelectedEnrollment = new EnrollmentModel();
                UpdateAppStatus("New enrollment created");
            }
            catch (Exception ex)
            {
                UpdateAppStatus(ex.Message);
            }
        }
        public void SaveEnrollment()
        {
            try
            {
                var enrollmentDictionary = _enrollments.ToDictionary(key => key.EnrollmentId);

                if (SelectedEnrollment != null)
                {
                    _enrollmentCommand.Upsert(SelectedEnrollment);
                    Enrollments.Clear();
                    Enrollments.AddRange(_enrollmentCommand.GetList());

                    UpdateAppStatus("Enrollment saved");
                }
            }
            catch (Exception ex)
            {
                UpdateAppStatus(ex.Message);
            }
        }

        private void UpdateAppStatus(string message)
        {
            AppStatus = message;
            NotifyOfPropertyChange(() => AppStatus);
        }
    }
}
