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
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Book> Books { get; set; }
        public RestCollection<Author> Authors { get; set; }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set 
            {
                if (value != null)
                {
                    selectedBook = new Book
                    {
                        Title = value.Title,
                        BookId = value.BookId,
                        AuthorId = value.AuthorId,
                        Genre = value.Genre,
                        PublicationYear = value.PublicationYear
                    };
                    OnPropertyChanged();
                    (DeleteBookCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public ICommand CreateBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand UpdateBookCommand { get; set; }

        public MainWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                
                Books = new RestCollection<Book>("http://localhost:4356/", "book", "hub");
                CreateBookCommand = new RelayCommand(
                    () =>
                    {
                        Books.Add(new Book()
                        {
                            Title = SelectedBook.Title,
                            Genre = selectedBook.Genre,
                            PublicationYear = selectedBook.PublicationYear
                        });
                    }
                    );

                UpdateBookCommand = new RelayCommand(
                    () =>
                    {
                        Books.Update(SelectedBook);
                    }
                    );

                DeleteBookCommand = new RelayCommand(
                    () =>
                    {
                        Books.Delete(SelectedBook.BookId);
                    },
                    () =>
                    {
                        return SelectedBook != null;
                    }
                    );
                SelectedBook = new Book();
            }
            
        }
    }
}
