using System;
using CheckThingsAPI.Entities;
using CheckThingsAPI.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CheckThingsAPI.Models
{
    [BsonCollection("Games")]
    public class Game : Document
    {
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
    }
}