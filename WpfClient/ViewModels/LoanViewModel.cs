using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using R9IOPN_HFT_2023241.Models;

namespace WpfClient.ViewModels
{
    public class LoanViewModel :ObservableRecipient
    {
        public RestCollection<Loan> Loans { get; set; }
        private Loan selectedLoan;
        public ICommand CreateLoanCommand { get; set; }
        public ICommand UpdateLoanCommand { get; set; }
        public ICommand DeleteLoanCommand { get; set; }

        public Loan SelectedLoan
        {
            get
            {
                return selectedLoan;
            }
            set
            {
                if (value != null)
                {
                    selectedLoan = new Loan
                    {
                        LoanId = value.LoanId,
                        UserId = value.UserId,
                        User = value.User,
                        BookId = value.BookId,
                        Book = value.Book,
                        LoanDate = value.LoanDate,
                        ReturnDate = value.ReturnDate
                    };
                    OnPropertyChanged();
                    (DeleteLoanCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public LoanViewModel()
        {
            if (!IsInDesignMode)
            {
                Loans = new RestCollection<Loan>("http://localhost:4356/", "loan", "hub");
                CreateLoanCommand = new RelayCommand(
                () =>
                {
                    Loans.Add(new Loan()
                    {
                        UserId = SelectedLoan.UserId,
                        BookId = SelectedLoan.BookId,
                        LoanDate = SelectedLoan.LoanDate,
                        ReturnDate = SelectedLoan.ReturnDate

                    });
                }
                    );

                UpdateLoanCommand = new RelayCommand(
                () =>
                {
                    Loans.Update(SelectedLoan);
                }
                    );

                DeleteLoanCommand = new RelayCommand(
                () =>
                {
                    Loans.Delete(SelectedLoan.LoanId);
                },
                    () =>
                    {
                        return SelectedLoan != null;
                    }
                    );
                SelectedLoan = new Loan();
            }
        }
    }
}
