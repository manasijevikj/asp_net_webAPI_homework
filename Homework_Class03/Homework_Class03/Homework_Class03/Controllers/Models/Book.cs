using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Class03.Controllers.Models
{
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }

    }
}
