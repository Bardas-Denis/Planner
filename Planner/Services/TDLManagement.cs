using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Planner.Services
{
    class TDLManagement
    {
        private TreeView _treeView;
      
        public TDLManagement(TreeView treeView)
        {
            _treeView = treeView;
        }

        public void AddTDL(TDL tdl)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            dataSource.Add(tdl);
        }
        public void AddSubTDL(TDL tdl1, TDL tdl2)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.FirstOrDefault(ob => ob.Name == tdl2.Name) != default)
            {
                TDL td = dataSource.FirstOrDefault(ob => ob.Name == tdl2.Name);
                td.SubTDL.Add(tdl1);
            }
            else
            {
                TDL td1 = tdl2.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl2.Name);
                td1.SubTDL.Add(tdl1);
            }
        }
        public void RemoveTDL(TDL tdl)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.FirstOrDefault(ob => ob.Name == tdl.Name) != default)
            {
                TDL td = dataSource.First(ob => ob.Name == tdl.Name);
                dataSource.Remove(td);
            }
            else
            {
                tdl.Parent.SubTDL.Remove(tdl);
            }
        }
        public void Edit(TDL tdl, string name)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.FirstOrDefault(ob => ob.Name == tdl.Name) != default)
            {
                TDL td = dataSource.First(ob => ob.Name == tdl.Name);
                td.Name = name;
            }
            else
            {
                TDL td1 = tdl.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl.Name);
                td1.Name = name;
            }
        }
        public void MoveUpTDL(TDL tdl)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.IndexOf(tdl) != -1)
            {
                int index = dataSource.IndexOf(tdl);
                if (index != 0)
                {
                    dataSource.Move(index, index - 1);
                }
            }
            else
            {
                int index = tdl.Parent.SubTDL.IndexOf(tdl);
                if (index != 0)
                {
                    tdl.Parent.SubTDL.Move(index, index - 1);
                }
            }
        }
        public void MoveDownTDL(TDL tdl)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.IndexOf(tdl) != -1)
            {
                int index = dataSource.IndexOf(tdl);
                if (index != dataSource.Count - 1)
                {
                    dataSource.Move(index, index + 1);
                }
            }
            else
            {
                int index = tdl.Parent.SubTDL.IndexOf(tdl);
                if (index != tdl.Parent.SubTDL.Count - 1)
                {
                    tdl.Parent.SubTDL.Move(index, index + 1);
                }
            }
        }
        public void ChangePath(TDL tdl, string name)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (name == "")
            {
                TDL td1 = tdl.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl.Name);
                TDL tdAux = td1;
                td1.Parent.SubTDL.Remove(td1);
                tdAux.Parent = null;
                dataSource.Add(tdAux);
            }
            else
            {
                if (dataSource.FirstOrDefault(ob => ob.Name == name) != default)
                {
                    if (dataSource.FirstOrDefault(ob => ob.Name == tdl.Name) != default)
                    {
                        TDL td = dataSource.First(ob => ob.Name == tdl.Name);
                        TDL tdAux = td;
                        dataSource.Remove(td);
                        tdAux.Parent = dataSource.FirstOrDefault(ob => ob.Name == name);
                        dataSource.FirstOrDefault(ob => ob.Name == name).SubTDL.Add(tdAux);
                    }
                    else
                    {
                        TDL td1 = tdl.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl.Name);
                        TDL tdAux = td1;
                        td1.Parent.SubTDL.Remove(td1);
                        tdAux.Parent = dataSource.FirstOrDefault(ob => ob.Name == name);
                        dataSource.FirstOrDefault(ob => ob.Name == name).SubTDL.Add(tdAux);
                    }
                }
                //else
                //{
                //}
            }
        }
        public void AddTask(TDL tdl, Task task)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.FirstOrDefault(ob => ob.Name == tdl.Name) != default)
            {
                tdl.Tasks.Add(task);
            }
            else
            {
                TDL td1 = tdl.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl.Name);
                td1.Tasks.Add(task);
            }
        }
        public void MoveUpTask(Task task)
        {
            int index = task.TaskParent.Tasks.IndexOf(task);
            if (index != 0)
            {
                task.TaskParent.Tasks.Move(index, index - 1);
            }
        }
        public void MoveDownTask(Task task)
        {
            int index = task.TaskParent.Tasks.IndexOf(task);
            if (index != task.TaskParent.Tasks.Count - 1)
            {
                task.TaskParent.Tasks.Move(index, index + 1);
            }
        }
        public void DeleteTask(Task task)
        {
            task.TaskParent.Tasks.Remove(task);
        }
        public void EditTask(Task task, string name, string description, string status, string priority, DateTime deadline)
        {
            task.TaskName = name;
            task.Description = description;
            task.Status = status;
            task.Priority = priority;
            task.Deadline = deadline;
        }
        public void archiveDB(string path)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            string filePath = path + ".xml";
            DataContractSerializer dcs = new DataContractSerializer(typeof(TDLVM));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                dcs.WriteObject(fileStream, new TDLVM(dataSource));
            }
        }
        public void openDB(string path)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(TDLVM));
            FileStream fileStr = new FileStream(path, FileMode.Open);
            TDLVM deserializedObject;
            using (XmlReader xmlReader = XmlReader.Create(fileStr))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(TDLVM));
                deserializedObject = (TDLVM)serializer.ReadObject(xmlReader);
                _treeView.ItemsSource = deserializedObject.ItemsCollection;
            }
            fileStr.Dispose();
        }
        public void newDB()
        {
            _treeView.ItemsSource = new ObservableCollection<TDL>();
        }
        public void findByName(ListView tasks, ListView parents, string name)
        {
            ObservableCollection<Task> tasksFound = new ObservableCollection<Task>();
            ObservableCollection<string> parentsOfTasks = new ObservableCollection<string>();
            foreach (TDL tdl in _treeView.ItemsSource)
            {
                var task = tdl.Tasks.FirstOrDefault(t => t.TaskName == name);
                if (task != null)
                {
                    tasksFound.Add(task);
                    string parentName = task.TaskParent.Name;
                    TDL parent = task.TaskParent;
                    while (parent.Parent != null)
                    {
                        parentName = parentName + "<-" + parent.Parent.Name;
                        parent = parent.Parent;
                    }
                    parentsOfTasks.Add(parentName);

                }
                FindTaskInSubTDLs(tdl.SubTDL, name, ref tasksFound, ref parentsOfTasks);
                tasks.ItemsSource = tasksFound;
                parents.ItemsSource = parentsOfTasks;
            }
        }
        private void FindTaskInSubTDLs(ObservableCollection<TDL> subTDLs, string taskName, ref ObservableCollection<Task> tasksFound, ref ObservableCollection<string> parentsOfTasks)
        {
            foreach (var tdl in subTDLs)
            {
                var task = tdl.Tasks.FirstOrDefault(t => t.TaskName == taskName);
                if (task != null)
                {
                    tasksFound.Add(task);
                    string parentName = task.TaskParent.Name;
                    TDL parent = task.TaskParent;
                    while (parent.Parent != null)
                    {
                        parentName = parentName + "<-" + parent.Parent.Name;
                        parent = parent.Parent;
                    }
                    parentsOfTasks.Add(parentName);
                }
                FindTaskInSubTDLs(tdl.SubTDL, taskName, ref tasksFound, ref parentsOfTasks);
            }
        }

        public void findByDeadline(ListView tasks, ListView parents, DateTime deadline)
        {
            ObservableCollection<Task> tasksFound = new ObservableCollection<Task>();
            ObservableCollection<string> parentsOfTasks = new ObservableCollection<string>();
            foreach (TDL tdl in _treeView.ItemsSource)
            {
                var task = tdl.Tasks.FirstOrDefault(t => t.Deadline == deadline);
                if (task != null)
                {
                    tasksFound.Add(task);
                    string parentName = task.TaskParent.Name;
                    TDL parent = task.TaskParent;
                    while (parent.Parent != null)
                    {
                        parentName = parentName + "<-" + parent.Parent.Name;
                        parent = parent.Parent;
                    }
                    parentsOfTasks.Add(parentName);

                }
                FindTaskInSubTDLsByDeadline(tdl.SubTDL, deadline, ref tasksFound, ref parentsOfTasks);
                tasks.ItemsSource = tasksFound;
                parents.ItemsSource = parentsOfTasks;
            }
        }
        private void FindTaskInSubTDLsByDeadline(ObservableCollection<TDL> subTDLs, DateTime deadline, ref ObservableCollection<Task> tasksFound, ref ObservableCollection<string> parentsOfTasks)
        {
            foreach (var tdl in subTDLs)
            {
                var task = tdl.Tasks.FirstOrDefault(t => t.Deadline == deadline);
                if (task != null)
                {
                    tasksFound.Add(task);
                    string parentName = task.TaskParent.Name;
                    TDL parent = task.TaskParent;
                    while (parent.Parent != null)
                    {
                        parentName = parentName + "<-" + parent.Parent.Name;
                        parent = parent.Parent;
                    }
                    parentsOfTasks.Add(parentName);
                }
                FindTaskInSubTDLsByDeadline(tdl.SubTDL, deadline, ref tasksFound, ref parentsOfTasks);
            }
        }
        public void SortByPriority(TDL tdl)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.FirstOrDefault(ob => ob.Name == tdl.Name) != default)
            {
                ObservableCollection<Task> tasks = tdl.Tasks;
                var priorityMap = new Dictionary<string, int> { { "Low", 1 }, { "Medium", 2 }, { "High", 3 } };
                tasks = new ObservableCollection<Task>(tasks.OrderBy(task => priorityMap[task.Priority]));
                tdl.Tasks = tasks;
            }
            else
            {
                TDL td1 = tdl.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl.Name);
                ObservableCollection<Task> tasks = td1.Tasks;
                var priorityMap = new Dictionary<string, int> { { "Low", 1 }, { "Medium", 2 }, { "High", 3 } };
                tasks = new ObservableCollection<Task>(tasks.OrderBy(task => priorityMap[task.Priority]));
                td1.Tasks = tasks;
            }
        }
        public void SortByDeadline(TDL tdl)
        {
            var dataSource = (ObservableCollection<TDL>)_treeView.ItemsSource;
            if (dataSource.FirstOrDefault(ob => ob.Name == tdl.Name) != default)
            {
                ObservableCollection<Task> tasks = tdl.Tasks;
                tasks = new ObservableCollection<Task>(tasks.OrderBy(task => task.Deadline));
                tdl.Tasks = tasks;
            }
            else
            {
                TDL td1 = tdl.Parent.SubTDL.FirstOrDefault(ob => ob.Name == tdl.Name);
                ObservableCollection<Task> tasks = td1.Tasks;
                tasks = new ObservableCollection<Task>(tasks.OrderBy(task => task.Deadline));
                td1.Tasks = tasks;
            }
        }
        public void FilterDone(ref ListView listView)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                Task task = (Task)item.DataContext;
                if (task.Status == "Done")
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void FilterDoneAndOverdue(ref ListView listView)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                Task task = (Task)item.DataContext;
                if (task.Status == "Done" && task.FinishTime > task.Deadline)
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void FilterToDoAndOverdue(ref ListView listView)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                Task task = (Task)item.DataContext;
                if (task.Status != "Done" && DateTime.Now > task.Deadline)
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void FilterToDoAndInTime(ref ListView listView)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                Task task = (Task)item.DataContext;
                if (task.Status != "Done" && DateTime.Now < task.Deadline)
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Collapsed;
                }
            }
        }
        public void ResetFilters(ref ListView listView)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                ListViewItem item = listView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                item.Visibility = Visibility.Visible;
            }
        }
        public void SetStatistics(ref int tasksDueToday, ref int tasksDueTomorrow, ref int tasksOverdue, ref int tasksDone, ref int tasksToDo)
        {
            foreach (TDL tdl in _treeView.ItemsSource)
            {
                foreach (Task task1 in tdl.Tasks)
                {
                    if (task1.Status == "Done")
                    {
                        tasksDone++;
                    }
                    else
                    {
                        tasksToDo++;
                    }
                    if (task1.Deadline == DateTime.Today)
                    {
                        tasksDueToday++;
                    }
                    else if (task1.Deadline == DateTime.Today.AddDays(1))
                    {
                        tasksDueTomorrow++;
                    }
                    if (task1.Deadline < DateTime.Now)
                    {
                        tasksOverdue++;
                    }


                }
                SetStatsSubTDL(tdl.SubTDL, ref tasksDueToday, ref tasksDueTomorrow, ref tasksOverdue, ref tasksDone, ref tasksToDo);
            }
        }
        public void SetStatsSubTDL(ObservableCollection<TDL> subTDLs, ref int tasksDueToday, ref int tasksDueTomorrow, ref int tasksOverdue, ref int tasksDone, ref int tasksToDo)
        {
            foreach (var tdl in subTDLs)
            {
                foreach (Task task1 in tdl.Tasks)
                {
                    if (task1.Status == "Done")
                    {
                        tasksDone++;
                    }
                    else
                    {
                        tasksToDo++;
                    }
                    if (task1.Deadline == DateTime.Today)
                    {
                        tasksDueToday++;
                    }
                    else if (task1.Deadline == DateTime.Today.AddDays(1))
                    {
                        tasksDueTomorrow++;
                    }
                    if (task1.Deadline < DateTime.Now)
                    {
                        tasksOverdue++;
                    }
                }
                SetStatsSubTDL(tdl.SubTDL, ref tasksDueToday, ref tasksDueTomorrow, ref tasksOverdue, ref tasksDone, ref tasksToDo);
            }
        }
    }
}

