using BookManager_BLL.Services;
using BookManager_DAL.Entities;
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

namespace BookManagerGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BooksService _service = new();

        public UserAccount currentAccount { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            HelloMsgLable.Content = "Hello, " + currentAccount.FullName;
            if (currentAccount.Role == 2)
            {
                CreateButton.IsEnabled = false;
                UpdateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void LoadGrid()
        {
            BookListsDataGrid.ItemsSource = null; //xóa trắng grid
                                                  // ko có nguồn data đưa vào
            BookListsDataGrid.ItemsSource = _service.GetAllBooks(); // đổ lại 
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DetailsWindow detail = new DetailsWindow();
            detail.ShowDialog();
            if (detail.DialogResult == true)
                
                LoadGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Book selected = BookListsDataGrid.SelectedItem as Book;
            if (selected == null)
            {
                MessageBox.Show("Please select a book before editing", "Select one", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//thoát hàm không mở form detail 
            }

            DetailsWindow detail = new();
            //trước khi render, chuyển cuốn sách đã chọn
            detail.SelectedBook = selected;
            detail.ShowDialog();
            if (detail.DialogResult == true)
                LoadGrid();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Book selected = BookListsDataGrid.SelectedItem as Book;
            if (selected == null)
            {
                MessageBox.Show("Please select a book before deleting", "Select one", MessageBoxButton.OK, MessageBoxImage.Error);
                return;//thoát hàm không mở form detail 
            }
            var answer = MessageBox.Show("Do you really want to DELETE?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No)
                return;

            _service.DeleteBook(selected);
            //F5
            LoadGrid();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to exit?", "Exit?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bookName = SearchNameTextBox.Text.ToLower();
            string bookDecs = SearchDescTextBox.Text.ToLower();
            List<Book> result = _service.SearchBooksByNameAndDecs(bookName, bookDecs);

            BookListsDataGrid.ItemsSource = null;
            BookListsDataGrid.ItemsSource = result;
        }
    }
}