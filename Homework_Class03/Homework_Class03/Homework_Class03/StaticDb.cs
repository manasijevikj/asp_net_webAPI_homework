using Homework_Class03.Controllers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Class03
{
    public class StaticDb
    {
        public static List<Book> Books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Author = "Stephen King",
                Title = "The Stand"
            },

            new Book
            {
                Id = 2,
                Author = "Brandon Sanderson",
                Title = "The Way of Kings"
            },

            new Book
            {
                Id = 3,
                Author = "Patrick Rothfuss",
                Title = "The Name of the Wind"
            },

        };
    }
}

