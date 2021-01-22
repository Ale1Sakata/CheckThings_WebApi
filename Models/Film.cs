using CheckThingsAPI.Entities;
using CheckThingsAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Models
{
    [BsonCollection("Films")]
    public class Film : Document
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
    }
}
