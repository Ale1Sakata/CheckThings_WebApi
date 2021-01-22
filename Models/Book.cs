using CheckThingsAPI.Entities;
using CheckThingsAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Models
{
    [BsonCollection("Books")]
    public class Book : Document
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
    }
}
