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
using static System.Reflection.Metadata.BlobBuilder;

namespace WpfClient.ViewModels
{
    public class AuthorViewModel : ObservableRecipient
    {
        public RestCollection<Author> Authors { get; set; }

        private Author selectedAuthor;

        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set 
            {
                if (value != null)
                {
                    selectedAuthor = new Author
                    {
                        Name = value.Name,
                        AuthorId = value.AuthorId,
                        BirthDate = value.BirthDate,
                        Books = value.Books,
                        Country = value.Country
                    };
                    OnPropertyChanged();
                    (DeleteAuthorCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public ICommand CreateAuthorCommand { get; set; }
        public ICommand UpdateAuthorCommand { get; set; }
        public ICommand DeleteAuthorCommand { get; set; }


        public AuthorViewModel()
        {
            if (!IsInDesignMode)
            {

                Authors = new RestCollection<Author>("http://localhost:4356/", "author", "hub");
                CreateAuthorCommand = new RelayCommand(
                () =>
                    {
                        Authors.Add(new Author()
                        {
                            Name = SelectedAuthor.Name,

                        });
                    }
                    );

                UpdateAuthorCommand = new RelayCommand(
                () =>
                {
                    Authors.Update(SelectedAuthor);
                    }
                    );

                DeleteAuthorCommand = new RelayCommand(
                () =>
                {
                    Authors.Delete(SelectedAuthor.AuthorId);
                    },
                    () =>
                    {
                        return SelectedAuthor != null;
                    }
                    );
                SelectedAuthor = new Author();
            }

        }
    }
}
