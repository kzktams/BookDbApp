using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;

namespace WpfClient.ViewModels
{
    public class HomeViewModel
    {
        public ICommand OpenBooks { get; set; }
        public ICommand OpenAuthors { get; set; }
        public ICommand OpenLoans { get; set; }
        public ICommand OpenUsers { get; set; }
        public ICommand OpenNonCruds { get; set; }

        public HomeViewModel()
        {
            OpenBooks = new RelayCommand(
                ()=>
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                }
                );
            OpenAuthors = new RelayCommand(
                () =>
                {
                    AuthorView au = new AuthorView();
                    au.Show();
                }
                );
            OpenLoans = new RelayCommand(
                () =>
                {
                    LoanView loan = new LoanView();
                    loan.Show();
                }
                );
            OpenUsers = new RelayCommand(
                () =>
                {
                    UserView us = new UserView();
                    us.Show();
                }
                );
            OpenNonCruds = new RelayCommand(
                () =>
                {
                    NonCrudView nc = new NonCrudView();
                    nc.Show();
                }
                );
        }
    }
}
