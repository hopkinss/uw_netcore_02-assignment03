using Assignment03.DataProvider;
using Assignment03.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Assignment03.ViewModels
{
    public class ViewModel : NotifyDataErrorInfoBase
    {
        private enum SortBy { Name = 1, Pass }
        public enum PassWordStrength { Fail = 0, Poor, Fair, Excellent }
        private IUserDataService userDataService;
        private User selectedUser;
        private bool isPasswordSortAscending;
        private bool isNameSortAscending;
        private ObservableCollection<User> users;
        private string name;
        private string password;
        private PassWordStrength passStrength;
        private bool isUserReadOnly;

        public ViewModel(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
            Users = new ObservableCollection<User>();

            SortCommand = new DelegateCommand<object>(OnSort);
            CreateUserCommand = new DelegateCommand(OnCreateUserExecute);
            SaveCommand = new DelegateCommand(OnSaveExecute, CanSaveExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, CanCancelExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, CanDeleteExecute);

            IsPasswordSortAscending = false;
            IsUserReadOnly = true;
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
                ValidateProperty("Name");
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
                ValidateProperty("Password");
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                SetPasswordStrength();
            }
        }
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                if (SaveCommand != null)
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if (selectedUser != null)
                {
                    Name = selectedUser.Name;
                    Password = selectedUser.Password;
                    OnPropertyChanged();
                    ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();

                    IsUserReadOnly = string.IsNullOrEmpty(selectedUser.Name);
                }
            }
        }
        public bool IsPasswordSortAscending
        {
            get { return isPasswordSortAscending; }
            set
            {
                isPasswordSortAscending = value;
                OnPropertyChanged();
            }
        }
        public bool IsNameSortAscending
        {
            get { return isNameSortAscending; }
            set
            {
                isNameSortAscending = value;
                OnPropertyChanged();
            }
        }
        public bool IsUserReadOnly
        {
            get { return isUserReadOnly; }
            set
            {
                isUserReadOnly = value;
                OnPropertyChanged();
            }
        }
        public PassWordStrength PassStrength
        {
            get { return passStrength; }
            set
            {
                passStrength = value;
                OnPropertyChanged();
            }
        }

        public ICommand SortCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }
        public void Load()
        {
            foreach (var v in userDataService.GetAllUsers())
            {
                Users.Add(v);
            }
        }

        private void SetPasswordStrength()
        {
            if (Password != null)
            {
                if (GetErrors("Password") == null)
                {
                    var pw = Password.Length;
                    if (pw < 5)
                    {
                        PassStrength = PassWordStrength.Poor;
                    }
                    else if (pw < 7)
                    {
                        PassStrength = PassWordStrength.Fair;
                    }
                    else if (pw >= 7)
                    {
                        PassStrength = PassWordStrength.Excellent;
                    }
                }
                else
                {
                    PassStrength = PassWordStrength.Fail;
                }
            }
        }


        private void OnSort(object sender)
        {
            ClearSelectedUser();
            var temp = new ObservableCollection<User>();
            temp.AddRange(Users);

            var sortBy = sender.ToString() == "Name" ? SortBy.Name : SortBy.Pass;

            if (sortBy == SortBy.Name)
            {
                temp = IsNameSortAscending ? new ObservableCollection<User>(temp.OrderBy(x => x.Name)) :
                                          new ObservableCollection<User>(temp.OrderByDescending(x => x.Name));
                IsNameSortAscending = !isNameSortAscending;
            }
            else
            {
                temp = IsPasswordSortAscending ? new ObservableCollection<User>(temp.OrderBy(x => x.Password)) :
                                          new ObservableCollection<User>(temp.OrderByDescending(x => x.Password));
                IsPasswordSortAscending = !isPasswordSortAscending;
            }
            Users.Clear();
            Users.AddRange(temp);
        }
        private void OnCreateUserExecute()
        {
            var user = new User();
            SelectedUser = user;
            ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
            IsUserReadOnly = false;

        }
        private void OnCancelExecute()
        {
            ClearSelectedUser();
            IsUserReadOnly = true;
        }
        private void OnSaveExecute()
        {
            if (!string.IsNullOrEmpty(selectedUser.Name))
            {
                selectedUser.Name = Name;
                selectedUser.Password = Password;
            }
            else
            {
                Users.Add(new User(Name, Password));
            }
            OnCancelExecute();
        }

        private void OnDeleteExecute()
        {
            Users.Remove(SelectedUser);
            OnCancelExecute();
        }

        private bool CanCancelExecute()
        {
            return !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Password) || HasErrors;
        }
        private bool CanDeleteExecute()
        {
            if (SelectedUser != null)
                return !string.IsNullOrEmpty(SelectedUser.Name);
            return false;
        }
        private bool CanSaveExecute()
        {
            if (SelectedUser != null)
            {
                return !HasErrors && (selectedUser.Name != Name || selectedUser.Password != Password);
            }

            return false;
        }


        private void ClearSelectedUser()
        {
            SelectedUser = null;
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
            Name = null;
            Password = null;
        }

        public void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);

            if (SelectedUser != null)
            {
                switch (propertyName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name))
                        {
                            AddError(propertyName, "Name is required");
                            return;
                        }

                        if (SelectedUser.Name == null && Users.FirstOrDefault(x => x.Name.ToLower() == Name.ToLower()) != default)
                        {
                            AddError(propertyName, $"We already have a {Name}");
                        }
                        break;

                    case nameof(Password):

                        if (string.IsNullOrEmpty(Password))
                        {
                            AddError(propertyName, "Password is required");
                            return;
                        }
                        if (!Regex.IsMatch(Password, @"^(?:(?=.*[a-z])(?=.*[\d\W])).{2,}$"))
                        {
                            AddError(propertyName, "Must contain a number and letter");
                        }

                        break;
                }
            }
        }
    }
}
