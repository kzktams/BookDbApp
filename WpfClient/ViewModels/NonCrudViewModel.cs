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
    public class NonCrudViewModel : ObservableRecipient
    {
        RestService restService;
        public List<AuthorDetail> AuthorsByName { get; set; }
        public List<AuthorPopularity> AuthorPopularities { get; set; }
        public List<BookDetail> BooksLoanedBetweenDates { get; set; }
        public List<BookDetail> BooksByGenre { get; set; }
        public List<BookDetail> BooksByAuthor { get; set; }
        public List<BookLoanCount> MostLoanedBooks { get; set; }
        public List<UserActivity> UserActivities { get; set; }
        public List<UserLoanDetail> UserLoanDetails { get; set; }

        private IEnumerable<object> currentContent;
        public IEnumerable<object> CurrentContent
        {
            get => currentContent;
            set
            {
                if (SetProperty(ref currentContent, value))
                {
                    OnPropertyChanged(nameof(CurrentContent));
                }
            }
        }
        public ICommand AuthorPopuCommanf { get; set; }
        public ICommand ShowMostLoanedBooksCommand { get; set; }
        public ICommand ShowUserActivities { get; set; }
        public ICommand ShowAuthorsByName { get; set; }
        public ICommand ShowLoansBetweenDates { get; set; }
        public ICommand ShowByGenreCommand { get; set; }
        public ICommand ShowBooksByAuthor { get; set; }
        public ICommand ShowBooksByUser { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private string authorName;
        public string AuthorName
        {
            get => authorName;
            set 
            {
                SetProperty(ref authorName, value);
                (ShowAuthorsByName as RelayCommand).NotifyCanExecuteChanged();

            }

        }
        private int authorId;
        public int AuthorId
        {
            get => authorId;
            set
            {
                if (SetProperty(ref authorId, value))
                {
                    (ShowBooksByAuthor as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }

        private string authorIdInput;
        public string AuthorIdInput
        {
            get => authorIdInput;
            set
            {
                if (SetProperty(ref authorIdInput, value))
                {
                    if (int.TryParse(value, out int id))
                    {
                        AuthorId = id;
                    }
                    else
                    {
                        AuthorId = -1; // Invalid ID if parsing fails
                    }
                    (ShowBooksByAuthor as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }

       

        private int userId;
        public int UserId
        {
            get => userId;
            set
            {
                SetProperty(ref userId, value);
                (ShowBooksByUser as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        private string userIdInput;
        public string UserIdInput
        {
            get => userIdInput;
            set
            {
                if (SetProperty(ref userIdInput, value))
                {
                    if (int.TryParse(value, out int id) && id > 0)
                    {
                        UserId = id;
                    }
                    else
                    {
                        UserId = -1; // Invalid ID if parsing fails
                    }
                    (ShowBooksByUser as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }


        private string startDate;

        public string StartDate
        {
            get { return startDate; }
            set 
            { 
                startDate = value;
                SetProperty(ref startDate, value);
                (ShowLoansBetweenDates as RelayCommand).NotifyCanExecuteChanged();
            }

        }
        private string endDate;

        public string EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                SetProperty(ref endDate, value);
                (ShowLoansBetweenDates as RelayCommand).NotifyCanExecuteChanged();
            }

        }

        private string genre;

        public string Genre
        {
            get { return genre; }
            set
            {
                genre = value;
                SetProperty(ref genre, value);
                (ShowByGenreCommand as RelayCommand).NotifyCanExecuteChanged();
            }

        }


        public NonCrudViewModel()
        {
            if (!IsInDesignMode)
            {
                restService = new RestService("http://localhost:4356/");

                AuthorPopuCommanf = new RelayCommand(
                    () =>
                    {
                        CurrentContent = restService.Get<AuthorPopularity>("Stat/AuthorPopularities");
                        OnPropertyChanged(nameof(CurrentContent));
                    }
                    );
                ShowMostLoanedBooksCommand = new RelayCommand(
                    () =>
                    {
                        CurrentContent = restService.Get<BookLoanCount>("Stat/MostLoanedBooks");
                        OnPropertyChanged(nameof(CurrentContent));
                    }
                    );
                ShowUserActivities = new RelayCommand(
                    () =>
                    {
                        CurrentContent = restService.Get<UserActivity>("Stat/Activity");
                        OnPropertyChanged(nameof(CurrentContent));
                    }
                    );
                ShowAuthorsByName = new RelayCommand(
                () =>
                {
                    AuthorsByName = restService.Get<AuthorDetail>($"Stat/AuthorsByName/{AuthorName}");
                    CurrentContent = AuthorsByName;
                    if (AuthorsByName == null || AuthorsByName.Count == 0)
                    {
                        MessageBox.Show("No authors found with that name.", "Search Result");
                    }
                    OnPropertyChanged(nameof(CurrentContent));
                },
                () => !string.IsNullOrWhiteSpace(AuthorName)
            ) ;

                ShowLoansBetweenDates = new RelayCommand(
                    () =>
                    {
                        DateTime starter = DateTime.Parse(StartDate);
                        DateTime ender = DateTime.Parse(EndDate);
                        BooksLoanedBetweenDates = restService.Get<BookDetail>($"Stat/BooksLoanedBetweenDates/{starter},{ender}");
                        currentContent = BooksLoanedBetweenDates;
                        if (BooksLoanedBetweenDates.Count == 0)
                        {
                            MessageBox.Show("No loans found in this interval.", "Search Result");
                        }
                        OnPropertyChanged(nameof(CurrentContent));
                    },
                    ()=> !string.IsNullOrWhiteSpace(StartDate) && !string.IsNullOrWhiteSpace(StartDate)
                    );
                ShowByGenreCommand = new RelayCommand(
                    () =>
                    {
                        BooksByGenre = restService.Get<BookDetail>($"Stat/BooksByGenre/{Genre}");
                        CurrentContent = BooksByGenre;
                        if (BooksByGenre.Count == 0)
                        {
                            MessageBox.Show("No movie found with this genre.", "Search Result");
                        }
                        OnPropertyChanged(nameof(CurrentContent));
                    },
                    ()=> !string.IsNullOrWhiteSpace(Genre)
                    );

                ShowBooksByAuthor = new RelayCommand(
                    () =>
                    {
                        BooksByAuthor = restService.Get<BookDetail>($"Stat/BooksByAuthor/{AuthorId}");
                        CurrentContent = BooksByAuthor;
                        if (BooksByAuthor == null || BooksByAuthor.Count == 0)
                        {
                            MessageBox.Show("This author doesn't have a book in our database.", "Search Result");
                        }
                        OnPropertyChanged(nameof(CurrentContent));
                    },
                    () => AuthorId > 0
                );

                ShowBooksByUser = new RelayCommand(
                    () =>
                    {
                        UserLoanDetails = restService.Get<UserLoanDetail>($"Stat/BooksLoanedByUser/{UserId}");
                        CurrentContent = UserLoanDetails;
                        if (UserLoanDetails == null || UserLoanDetails.Count == 0)
                        {
                            MessageBox.Show("No books found for this user.", "Search Result");
                        }
                        OnPropertyChanged(nameof(CurrentContent));
                    },
                    () => UserId > 0
                );

            }
        }
        

    }
}
