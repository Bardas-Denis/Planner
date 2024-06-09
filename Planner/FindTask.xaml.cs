using Planner.Services;
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
    /// Interaction logic for FindTask.xaml
    /// </summary>
    public partial class FindTask : Window
    {
        TDLManagement management;
        public FindTask(TreeView _treeView)
        {
            InitializeComponent();
            management = new TDLManagement(_treeView);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(NameTextBox.Text!="")
            {
                management.findByName(tasks,parents,NameTextBox.Text);
            }
            else
            {
                MessageBox.Show("Please enter a name");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DeadlineTextBox.Text != "")
            {
                management.findByDeadline(tasks,parents, DateTime.Parse(DeadlineTextBox.Text));
            }
            else
            {
                MessageBox.Show("Please enter a deadline");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
