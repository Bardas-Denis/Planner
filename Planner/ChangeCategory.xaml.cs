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
using System.Windows.Shapes;

namespace Planner
{
    /// <summary>
    /// Interaction logic for ChangeCategory.xaml
    /// </summary>
    public partial class ChangeCategory : Window
    {
        public ChangeCategory()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(TaskCategoryTextBox.Text!="")
            {
                Category.Items.Add(TaskCategoryTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter a category first");
            }
            
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
