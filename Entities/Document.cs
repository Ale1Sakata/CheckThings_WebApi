using CheckThingsAPI.Entities.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Entities
{
    public class Document : IDocument
    {
        public string Id { get; set; }
    }
}
