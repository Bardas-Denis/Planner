using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.ViewModels
{
    public class FindTaskVM : BaseVM
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public ObservableCollection<string> Paths { get; set; }
        public FindTaskVM()
        {
            Tasks = new ObservableCollection<Task>();
            Paths = new ObservableCollection<string>();
           
        }
    }
}
