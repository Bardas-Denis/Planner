using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
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
using System.Xml.Linq;
using Planner.Services;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using Path = System.IO.Path;

namespace Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TDLManagement management;

        public MainWindow()
        {
            InitializeComponent();
            management = new TDLManagement(treeView);
        }

        private void TreeViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement clickedElement = (FrameworkElement)e.OriginalSource;
            TDL x = clickedElement.DataContext as TDL;
            TDLVM viewModel = DataContext as TDLVM;
            if (x != null)
            {
                viewModel.SelectedTasks=x.Tasks;
            }
        }

        private void ListView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement clickedElement = (FrameworkElement)e.OriginalSource;
            Task x = clickedElement.DataContext as Task;
            TDLVM viewModel = DataContext as TDLVM;
            if (x != null)
            {
                viewModel.SelectedTaskDescription=x.Description;
                
            }
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            FileToolBar.Visibility = Visibility.Visible;
            TDLToolBar.Visibility = Visibility.Collapsed;
            TaskToolBar.Visibility = Visibility.Collapsed;
            ViewToolBar.Visibility = Visibility.Collapsed;
            About.Visibility = Visibility.Collapsed;
        }

        private void TDL_Click(object sender, RoutedEventArgs e)
        {
            FileToolBar.Visibility = Visibility.Collapsed;
            TDLToolBar.Visibility = Visibility.Visible;
            TaskToolBar.Visibility = Visibility.Collapsed;
            ViewToolBar.Visibility = Visibility.Collapsed;
            About.Visibility = Visibility.Collapsed;
        }

        private void Task_Click(object sender, RoutedEventArgs e)
        {
            FileToolBar.Visibility = Visibility.Collapsed;
            TDLToolBar.Visibility = Visibility.Collapsed;
            TaskToolBar.Visibility = Visibility.Visible;
            ViewToolBar.Visibility = Visibility.Collapsed;
            About.Visibility = Visibility.Collapsed;
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            FileToolBar.Visibility = Visibility.Collapsed;
            TDLToolBar.Visibility = Visibility.Collapsed;
            TaskToolBar.Visibility = Visibility.Collapsed;
            ViewToolBar.Visibility = Visibility.Visible;
            About.Visibility = Visibility.Collapsed;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            FileToolBar.Visibility = Visibility.Collapsed;
            TDLToolBar.Visibility = Visibility.Collapsed;
            TaskToolBar.Visibility = Visibility.Collapsed;
            ViewToolBar.Visibility = Visibility.Collapsed;
            About.Visibility = Visibility.Visible;
        }

        private void addRoot_Click(object sender, RoutedEventArgs e)
        {
            AddTDL addTDL = new AddTDL();
            string name2="";
            addTDL.Closed += (s, args) =>
            {
                name2= addTDL.TDLNameTextBox.Text;
                string name = addTDL.TDLNameTextBox.Text;
                management.AddTDL(new TDL
                {
                    Name = name,
                    SubTDL = new ObservableCollection<TDL> { },
                    Tasks = new ObservableCollection<Task> { }
                });
            };
            
            addTDL.ShowDialog();
            MessageBox.Show(name2);
        }

        private void deleteTDL_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                management.RemoveTDL(treeView.SelectedItem as TDL);
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }
        }

        private void addSubTDL_Click(object sender, RoutedEventArgs e)
        {
            AddTDL addTDL = new AddTDL();
            if (treeView.SelectedItem != null)
            {
                addTDL.Closed += (s, args) =>
                {
                    string name = addTDL.TDLNameTextBox.Text;
                    management.AddSubTDL(new TDL
                    {
                        Name = name,
                        SubTDL = new ObservableCollection<TDL> { },
                        Tasks = new ObservableCollection<Task> { },
                        Parent = treeView.SelectedItem as TDL
                    },
                    treeView.SelectedItem as TDL
                    );
                };
                addTDL.Show();
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            AddTDL addTDL = new AddTDL();
            if (treeView.SelectedItem != null)
            {
                addTDL.Closed += (s, args) =>
                {
                    string name = addTDL.TDLNameTextBox.Text;
                    management.Edit(treeView.SelectedItem as TDL, name);
                };
                addTDL.Show();
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }

        }

        private void moveUpTDL_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                management.MoveUpTDL(treeView.SelectedItem as TDL);
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }
        }

        private void moveDownTDL_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                management.MoveDownTDL(treeView.SelectedItem as TDL);
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }
        }

        private void changePath_Click(object sender, RoutedEventArgs e)
        {
            AddTDL addTDL = new AddTDL();
            if (treeView.SelectedItem != null)
            {
                addTDL.Closed += (s, args) =>
                {
                    string name = addTDL.TDLNameTextBox.Text;
                    management.ChangePath(treeView.SelectedItem as TDL, name);
                };
                addTDL.Show();
                addTDL.Placeholder.Text = "Enter the name of the new parent";
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask();
            if (treeView.SelectedItem != null)
            {
                addTask.Closed += (s, args) =>
                {
                    if (addTask.TaskNameTextBox.Text == "" || addTask.TaskDescriptionTextBox.Text == "")
                    {
                        MessageBox.Show("Invalid");
                    }
                    else
                    {
                        string name = addTask.TaskNameTextBox.Text;
                        string description = addTask.TaskDescriptionTextBox.Text;
                        string status = addTask.Staus.Text;
                        string priority = addTask.Priority.Text;
                        DateTime deadline;
                        if (addTask.TaskDeadlineTextBox.Text != "")
                        {
                            deadline = DateTime.Parse(addTask.TaskDeadlineTextBox.Text);
                        }
                        else
                        {
                            deadline = DateTime.MaxValue;
                        }
                        string category = addTask.Category.Text;
                        management.AddTask(treeView.SelectedItem as TDL, new Task
                        {
                            TaskName = name,
                            Description = description,
                            Status = status,
                            Priority = priority,
                            Deadline = deadline,
                            TypeTask = category,
                            TaskParent = treeView.SelectedItem as TDL
                        });
                    }
                };
                addTask.Show();
            }
            else
            {
                MessageBox.Show("Please select a TDL");
            }

        }

        private void SetDone_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem != null)
            {
                Task task = Tasks.SelectedItem as Task;
                var listViewItem = Tasks.ItemContainerGenerator.ContainerFromItem(Tasks.SelectedItem) as ListViewItem;
                if (listViewItem != null)
                {
                    listViewItem.Background = Brushes.LightGreen;
                }
                task.Status = "Done";
                task.FinishTime = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Please select a task");
            }

        }

        private void TaskUp_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem != null)
            {
                Task task = Tasks.SelectedItem as Task;
                management.MoveUpTask(task);
            }
            else
            {
                MessageBox.Show("Please select a task");
            }
        }

        private void TaskDown_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem != null)
            {
                Task task = Tasks.SelectedItem as Task;
                management.MoveDownTask(task);
            }
            else
            {
                MessageBox.Show("Please select a task");
            }
        }

        private void changeCategory_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem != null)
            {
                Task task = Tasks.SelectedItem as Task;
                ChangeCategory changeCategory = new ChangeCategory();
                changeCategory.Closed += (s, args) =>
                {
                    string category = changeCategory.Category.Text;
                    task.TypeTask = category;
                };
                changeCategory.Show();
            }
            else
            {
                MessageBox.Show("Please select a task");
            }

        }

        private void deleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem != null)
            {
                Task task = Tasks.SelectedItem as Task;
                management.DeleteTask(task);
            }
            else
            {
                MessageBox.Show("Please select a task");
            }
        }

        private void editTask_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.SelectedItem != null)
            {
                EditTask editTask = new EditTask();
                editTask.Closed += (s, args) =>
                {
                    if (editTask.TaskNameTextBox.Text == "" || editTask.TaskDescriptionTextBox.Text == "")
                    {
                        MessageBox.Show("Invalid");
                    }
                    else
                    {
                        string name = editTask.TaskNameTextBox.Text;
                        string description = editTask.TaskDescriptionTextBox.Text;
                        string status = editTask.Staus.Text;
                        string priority = editTask.Priority.Text;
                        DateTime deadline = DateTime.MaxValue;
                        if (editTask.TaskDeadlineTextBox.Text != "")
                        {
                            deadline = DateTime.Parse(editTask.TaskDeadlineTextBox.Text);
                        }
                        management.EditTask(Tasks.SelectedItem as Task, name, description, status, priority, deadline);
                    }
                };
                editTask.Show();
            }
            else
            {
                MessageBox.Show("Please select a task");
            }
        }

        private void archiveDB_Click(object sender, RoutedEventArgs e)
        {
            ArchiveDB archiveDB = new ArchiveDB();
            archiveDB.Closed += (s, args) =>
            {
                string name = archiveDB.ArchiveNameTextBox.Text;
                management.archiveDB(name);

            };
            archiveDB.Show();
        }
        private void openDB_Click(object sender, RoutedEventArgs e)
        {
            OpenArchives openArchives = new OpenArchives();
            openArchives.Closed += (s, args) =>
            {
                string path = openArchives.Archives.Text;
                management.openDB(path);
            };
            openArchives.Show();
        }

        private void newDB_Click(object sender, RoutedEventArgs e)
        {
            management.newDB();
        }

        private void findTask_Click(object sender, RoutedEventArgs e)
        {
            FindTask findTask = new FindTask(treeView);
            findTask.Show();
        }

        private void sortByPriority_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                management.SortByPriority(treeView.SelectedItem as TDL);
                TDL x = treeView.SelectedItem as TDL;
                TDLVM viewModel = DataContext as TDLVM;
                if (x != null)
                {
                    viewModel.SelectedTasks = x.Tasks;
                }
            }
        }

        private void sortByDeadline_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                management.SortByDeadline(treeView.SelectedItem as TDL);
                TDL x = treeView.SelectedItem as TDL;
                TDLVM viewModel = DataContext as TDLVM;
                if (x != null)
                {
                    viewModel.SelectedTasks = x.Tasks;
                }
            }
        }
        
        private void filterTasks_Click(object sender, RoutedEventArgs e)
        {
            ChooseFilter chooseFilter = new ChooseFilter();
            chooseFilter.Closed += (s, args) =>
            {
                string choosenOption = chooseFilter.Options.Text;
                if(choosenOption== "Show tasks that are done")
                {
                    management.FilterDone(ref Tasks);
                }
                else if(choosenOption== "Show done and overdue")
                {
                    management.FilterDoneAndOverdue(ref Tasks);
                }
                else if(choosenOption== "Show overdue")
                {
                    management.FilterToDoAndOverdue(ref Tasks);
                }
                else if(choosenOption== "Show unfinished but still before deadline")
                {
                    management.FilterToDoAndInTime(ref Tasks);
                }
            };
            chooseFilter.Show();
        }

        private void resetFilters_Click(object sender, RoutedEventArgs e)
        {
            management.ResetFilters(ref Tasks);
        }

        private void updateStats_Click(object sender, RoutedEventArgs e)
        {
            TDLVM viewModel = DataContext as TDLVM;
            int tasksDueToday=0;
            int tasksDueTomorrow=0;
            int tasksOverdue = 0;
            int tasksDone = 0;
            int tasksToDo = 0;
            management.SetStatistics(ref tasksDueToday,ref tasksDueTomorrow,ref tasksOverdue,ref tasksDone,ref tasksToDo);
            viewModel.TasksDueTomorrow = tasksDueTomorrow; 
            viewModel.TasksDueToday = tasksDueToday;
            viewModel.TasksOverdue = tasksOverdue;
            viewModel.TasksToDo = tasksToDo;
            viewModel.TasksDone = tasksDone;
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bardas Denis-Adelin, Grupa 211");
        }
    }
}
