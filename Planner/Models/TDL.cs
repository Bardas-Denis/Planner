using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Planner
{
    [DataContract]
    public class TDL: BaseVM
    {
        [DataMember]
        public ObservableCollection<TDL> SubTDL { get; set; }
        [DataMember]
        public ObservableCollection<Task> Tasks { get; set; }
        [DataMember]
        private string name;
        [DataMember]
        private TDL parent;
        
        public TDL() 
        {
            SubTDL = new ObservableCollection<TDL>();
            Tasks= new ObservableCollection<Task>();
        }
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public TDL Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
                NotifyPropertyChanged("Parent");
            }
        }
    }
}
