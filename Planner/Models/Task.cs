using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Planner
{
    [DataContract(IsReference = true)]
    public class Task : BaseVM
    {
        [DataMember]
        private string taskName;
        [DataMember]
        private string description;
        [DataMember]
        private string status;
        [DataMember]
        private string priority;
        [DataMember]
        private string typeTask;
        [DataMember]
        private DateTime deadline;
        [DataMember]
        private DateTime finishTime;
        [DataMember]
        private TDL taskParent;
       
        public TDL TaskParent
        {
            get
            {
                return taskParent;
            }
            set
            {
                taskParent = value;
                NotifyPropertyChanged("TaskParent");
            }
        }
        public string TaskName
        {
            get
            {
                return taskName;
            }
            set
            {
                taskName = value;
                NotifyPropertyChanged("TaskName");
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged("Description");
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }
        public string Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                NotifyPropertyChanged("Priority");
            }
        }
        public string TypeTask
        {
            get { return typeTask; }
            set
            {
                typeTask = value;
                NotifyPropertyChanged("TypeTask");
            }
        }
        public DateTime Deadline
        {
            get { return deadline; }
            set
            {
                deadline = value;
                NotifyPropertyChanged("Deadline");
            }
        }
        public DateTime FinishTime
        {
            get { return finishTime; }
            set
            {
                finishTime = value;
                NotifyPropertyChanged("FinishTime");
            }
        }
    }
    }
