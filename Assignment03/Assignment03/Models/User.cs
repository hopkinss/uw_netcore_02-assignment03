using Assignment03.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Assignment03.ViewModels;
using Prism.Commands;

namespace Assignment03.Models
{
    public class User: NotifyDataErrorInfoBase
    {
        private string name;
        private string password;

        public User(string name,string password)
        {
            this.name = name;
            this.password = password;
        }
        public User()
        {

        }

        public string Password 
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
 
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();


            }
        }


    }
}
