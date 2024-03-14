using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using R9IOPN_HFT_2023241.Models;

namespace WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Book> Books { get; set; }

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set 
            { 
                SetProperty(ref selectedBook, value);
                (DeleteBookCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
        public ICommand UpdateBookCommand { get; set; }

        public MainWindowViewModel()
        {
            Books = new RestCollection<Book>("http://localhost:4356/","book");
            CreateBookCommand = new RelayCommand(
                () =>
                {
                    Books.Add(new Book()
                    {
                        Genre = "Mystery",
                        Title = "new title"
                        
                    });
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
        }
    }
}
