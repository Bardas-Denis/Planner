using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.Serialization;
using System.Windows;
using Planner.Services;

namespace Planner
{
    [DataContract]
    public class TDLVM : BaseVM
    {
        [DataMember]
        public ObservableCollection<TDL> ItemsCollection { get; set; }
        private ObservableCollection<Task> selectedTasks { get; set; }
        private TDL selectedItem;
        private string selectedTaskDescription;
        public int tasksDueToday;
        public int tasksDueTomorrow;
        public int tasksOverdue;
        public int tasksDone;
        public int tasksToDo;
        public TDLVM(ObservableCollection<TDL> items)
        {
            ItemsCollection = items;
        }
        public TDLVM()
        {
            ItemsCollection = new ObservableCollection<TDL>();
        }
        public int TasksDueToday
        {
            get
            {
                return tasksDueToday;
            }
            set
            {
                tasksDueToday = value;
                NotifyPropertyChanged("TasksDueToday");
            }
        }
        public int TasksDueTomorrow
        {
            get { return tasksDueTomorrow; }
            set
            {
                tasksDueTomorrow = value;
                NotifyPropertyChanged("TasksDueTomorrow");
            }
        }
        public int TasksOverdue
        {
            get { return tasksOverdue; }
            set
            {
                tasksOverdue = value;
                NotifyPropertyChanged("TasksOverdue");
            }
        }
        public int TasksDone
        {
            get { return tasksDone; }
            set
            {
                tasksDone = value;
                NotifyPropertyChanged("TasksDone");
            }
        }
        public int TasksToDo
        {
            get { return tasksToDo; }
            set
            {
                tasksToDo = value;
                NotifyPropertyChanged("TasksToDo");
            }
        }
        public TDL SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<Task> SelectedTasks
        {
            get { return selectedTasks; }
            set
            {
                selectedTasks = value;
                NotifyPropertyChanged(nameof(SelectedTasks));
            }
        }
        public string SelectedTaskDescription
        {
            get { return selectedTaskDescription; }
            set
            {
                selectedTaskDescription = value;
                NotifyPropertyChanged(nameof(SelectedTaskDescription));
            }
        }
        
    }
}
