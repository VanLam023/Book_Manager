using BookManager_BLL.Services;
using BookManager_DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookManagerGui
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private BookCategoryService _cateService = new();
        private BooksService _service = new();

        public Book SelectedBook { private get; set; } = null;
        public DetailsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //đổ data vào combo box bất chấp tạo mới hay edit book
            //xài có 1 lần việc đổ, ko cần tách làm 
            //dĩ nhiên cần gọi CategoryService trợ giúp
            BookCategoryIdComboBox.ItemsSource = _cateService.GetAllCategory();
            BookCategoryIdComboBox.DisplayMemberPath = "BookGenreType";
            BookCategoryIdComboBox.SelectedValuePath = "BookCategoryId";
            //CHƠI DEFAULT CHO TYPE 1 KHI TẠO MỚI 
            BookCategoryIdComboBox.SelectedValue = 1;
            WindowModeLabel.Content = "Create a new book";

            //KIỂM TRA COI CÓ PHẢI LÀ EDIT/UPDATE MODE HAY KHÔNG, NẾU ĐÚNG THÌ FILL DATA CỦA CUỐN SÁCH MUỐN EDIT
            //NẾU LÀ CREATE MODE, THÌ CHẲNG CẦN LÀM GÌ THÊM, ĐỂ FORM TRẮNG TRƠN
            if (SelectedBook != null)
            {
                //TA PHẢI DISABLE KHÔNG CHO USER EDIT BOOK IDOWR MODE EDIT
                //TA CẬP NHẬT LẠI LABEL ĐỂ THÔNG BÁO RÕ MÀN HÌNH GÌ VỚI USER 

                WindowModeLabel.Content = "Update the Selected Book";

                BookIdTextBox.Text = SelectedBook.BookId.ToString();
                BookIdTextBox.IsEnabled = false; //cấm sửa 

                BookNameTextBox.Text = SelectedBook.BookName;
                DescriptionTextBox.Text = SelectedBook.Description;
                PublicationDateDatePicker.Text = SelectedBook.PublicationDate.ToString();
                QuantityTextBox.Text = SelectedBook.Quantity.ToString();
                PriceTextBox.Text = SelectedBook.Price.ToString();
                AuthorTextBox.Text = SelectedBook.Author;
                //jump nhảy đến đúng  Category mà cuốn sách đang có
                BookCategoryIdComboBox.SelectedValue = SelectedBook.BookCategoryId.ToString();
                //quan trọng, nếu không mọi cuốn sách edit đều về category đầu tiên 1-FICTION TYPE 
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookIdTextBox.Text.IsNullOrEmpty() || BookNameTextBox.Text.IsNullOrEmpty() || DescriptionTextBox.Text.IsNullOrEmpty()
                    || QuantityTextBox.Text.IsNullOrEmpty() || PriceTextBox.Text.IsNullOrEmpty() || AuthorTextBox.Text.IsNullOrEmpty() || PublicationDateDatePicker.SelectedDate.HasValue)
            {
                System.Windows.MessageBox.Show("All field must be not null", "Invalid data", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Regex.IsMatch(QuantityTextBox.Text, @"[0-9]*"))
            {
                System.Windows.MessageBox.Show("The quantity must be a number", "Invalid quantity", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(PriceTextBox.Text, @"[0-9]*"))
            {
                System.Windows.MessageBox.Show("The price must be a number", "Invalid price", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int quantity = int.Parse(QuantityTextBox.Text);
            if (quantity <= 0 || quantity >= 4_000_000)
            {
                MessageBox.Show("The quantity must be between 0 ... 4,000,000", "Invalid quantity", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int price = int.Parse(PriceTextBox.Text);
            if (price <= 0)
            {
                System.Windows.MessageBox.Show("The price must be greater than 0", "Invalid price", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //MessageBox.Show("You choose Cate: " + BookCategoryIdComboBox.SelectedValue);
            Book x = new Book();
            x.BookId = int.Parse(BookIdTextBox.Text);
            x.BookName = BookNameTextBox.Text;
            x.Description = DescriptionTextBox.Text;
            x.PublicationDate = DateTime.Now;//PublicationDateDatePicker.SelectedDate as Date;
            x.Quantity = int.Parse(QuantityTextBox.Text);
            x.Price = double.Parse(PriceTextBox.Text);
            x.Author = AuthorTextBox.Text;
            x.BookCategoryId = int.Parse(BookCategoryIdComboBox.SelectedValue.ToString());
            //KIỂM TRA XEM MODE GÌ? EDIT/IPDATE HAY CREATE, THÔNG QUA BIẾN FLAG
            if (SelectedBook != null)
                _service.UpdateBook(x);
            else
                _service.AddBook(x);
            DialogResult = true; //BẬT CỜ, PHẤT CỜ NẾU NHẤN SAVE 
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; //biến/prop có sẵn của class này thừa kế từ class cha Window mang giá trị true/false 
                                  //mình dùng nó để lưu status nhấn nút SAVE HAY CANCEL 
            this.Close();
            //code ngon thì phải biết khi màn hình này đóng qua nút Close tức là ko có sửa gì data thì ko cần F5 LẠI GRID BÊN MAIN 
            //NẾU NHẤN SALE CHẮC CHẮN CÓ CHỈNH SỬA, HOẶC TẠO MỚI GÌ ĐÓ TA MỚI F5 GRID 
            //GÓC NHÌN PERFORMANCE 
        }
    }
}
