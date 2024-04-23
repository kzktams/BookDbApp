using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using R9IOPN_HFT_2023241.Models;

namespace WpfClient.ViewModels
{
    public class UserViewModel : ObservableRecipient
    {
        public RestCollection<User> Users { get; set; }
        private User selectedUser;
        public ICommand CreateUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }

        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                if (value != null)
                {
                    selectedUser = new User
                    {
                        UserId = value.UserId,
                        Name = value.Name,
                        Email = value.Email,
                        Phone = value.Phone,
                        Loans = value.Loans
                    };
                    OnPropertyChanged();
                    (DeleteUserCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public UserViewModel()
        {
            if (!IsInDesignMode)
            {
                Users = new RestCollection<User>("http://localhost:4356/", "user", "hub");
                CreateUserCommand = new RelayCommand(
                () =>
                {
                    Users.Add(new User()
                    {
                        Name = SelectedUser.Name,
                        Email = selectedUser.Email,
                        Phone = selectedUser.Phone

                    });
                }
                    );

                UpdateUserCommand = new RelayCommand(
                () =>
                {
                    Users.Update(SelectedUser);
                }
                    );

                DeleteUserCommand = new RelayCommand(
                () =>
                {
                    Users.Delete(SelectedUser.UserId);
                },
                    () =>
                    {
                        return SelectedUser != null;
                    }
                    );
                SelectedUser = new User();
            }
        }
    }
}
