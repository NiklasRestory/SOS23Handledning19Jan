using SOS23Handledning19JanWPFDatakoppling.Items;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SOS23Handledning19JanWPFDatakoppling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();

        Dictionary<int, Student> students = new Dictionary<int, Student>();
        List<Student> studentList = new List<Student>();


        public MainWindow()
        {
            InitializeComponent();

            students = databaseConnection.GetStudents();
            studentList = students.Values.ToList();

            studentListBox.ItemsSource = students.Values;
            studentListBox.Items.Refresh();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int studentIndexToUpdate = studentListBox.SelectedIndex;
            if (studentIndexToUpdate != -1)
            {
                Student student = studentList[studentIndexToUpdate];
                studentListBox.SelectedIndex = -1;
                string newName = NewName.Text;
                bool success = databaseConnection.UpdateStudentName(student.Id, newName);

                if (success)
                {
                    student.Name = newName;
                    studentListBox.Items.Refresh();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int studentIndexToDelete = studentListBox.SelectedIndex;
            if (studentIndexToDelete != -1)
            {
                Student student = studentList[studentIndexToDelete];
                studentListBox.SelectedIndex = -1;
                bool success = databaseConnection.DeleteStudent(student.Id);

                if (success)
                {
                    studentList.Remove(student);
                    students.Remove(student.Id);
                    studentListBox.Items.Refresh();
                }
            }
        }
    }
}