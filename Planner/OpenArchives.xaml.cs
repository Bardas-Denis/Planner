using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace Planner
{
    /// <summary>
    /// Interaction logic for OpenArchives.xaml
    /// </summary>
    public partial class OpenArchives : Window
    {
        public OpenArchives()
        {
            InitializeComponent();
            string[] xmlFiles = Directory.GetFiles("../../bin/Debug", "*.xml");
            foreach (string file in xmlFiles)
            {
                
                string fileName = Path.GetFileName(file);
                if (fileName != "Microsoft.Xaml.Behaviors.xml")
                {
                    Archives.Items.Add(fileName);
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
