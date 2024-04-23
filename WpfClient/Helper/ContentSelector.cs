using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using R9IOPN_HFT_2023241.Models;

namespace WpfClient.Helper
{
    public class ContentSelector : DataTemplateSelector
    {
        public DataTemplate AuthorPopularityTemplate { get; set; }
        public DataTemplate BookLoanCountTemplate { get; set; }
        public DataTemplate UserActivityTemplate{ get; set; }
        public DataTemplate AuthorsByNameTemplate { get; set; }
        public DataTemplate BookDetailTemplate { get; set; }
        public DataTemplate UserLoanDetailsTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is AuthorPopularity)
                return AuthorPopularityTemplate;
            else if (item is BookLoanCount)
                return BookLoanCountTemplate;
            else if (item is UserActivity)
                return UserActivityTemplate;
            else if (item is AuthorDetail)
                return AuthorsByNameTemplate;
            else if (item is BookDetail)
                return BookDetailTemplate;
            else if (item is UserLoanDetail)
                return UserLoanDetailsTemplate;

            return null;
        }
    }
}
