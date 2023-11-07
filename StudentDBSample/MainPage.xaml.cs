using StudentDBSample.Data;

namespace StudentDBSample
{
    public partial class MainPage : ContentPage
    {
        private StudentRepository repository;
        private List<Student> students;

        public MainPage()
        {
            InitializeComponent();

            repository = new StudentRepository();
            LoadStudents();
        }

        private void LoadStudents()
        {
            students = repository.GetStudents();
            studentListView.ItemsSource = students;
        }

        private void AddStudent_Clicked(object sender, EventArgs e)
        {
            // Get student information from your input fields
            Student newStudent = new Student
            {
                Name = nameEntry.Text,
                Age = int.Parse(ageEntry.Text)
            };

            repository.AddStudent(newStudent);
            LoadStudents();
        }

        private void UpdateStudent_Clicked(object sender, EventArgs e)
        {
            if (studentListView.SelectedItem != null)
            {
                // Get selected student and update their information
                Student selectedStudent = (Student)studentListView.SelectedItem;
                selectedStudent.Name = nameEntry.Text;
                selectedStudent.Age = int.Parse(ageEntry.Text);

                repository.UpdateStudent(selectedStudent);
                LoadStudents();
            }
        }

        private void DeleteStudent_Clicked(object sender, EventArgs e)
        {
            if (studentListView.SelectedItem != null)
            {
                // Get selected student and delete them
                Student selectedStudent = (Student)studentListView.SelectedItem;
                repository.DeleteStudent(selectedStudent.Id);
                LoadStudents();
            }
        }
    }

}