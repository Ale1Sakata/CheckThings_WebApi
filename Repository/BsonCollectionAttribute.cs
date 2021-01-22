using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckThingsAPI.Repository
{
    public class BsonCollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
