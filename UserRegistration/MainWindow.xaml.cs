

using System;
using System.Collections.Generic;
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

namespace UserRegistration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as MainVM;

            //check if passwords are equal
            if (firstPasswordTextBox.Password != secondPasswordTextBox.Password)
            {
                HintsLabel.Content = "The passwords are not equal, they must match!";
                firstPasswordTextBox.Password = "";
                secondPasswordTextBox.Password = "";
                return;
            }

            context.registerUser(userNameTB.Text, firstPasswordTextBox.Password);
            UserListView.ItemsSource = context.UserList;
            HintsLabel.Content                  = "";
            userNameTB.Text                     = "";
            firstPasswordTextBox.Password       = "";
            secondPasswordTextBox.Password      = "";
        }
    }
}
