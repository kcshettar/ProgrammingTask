using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTreeHouse.Models;

//MongoDB - mLab cloud
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using Microsoft.Extensions.Configuration.Json;

namespace WebApplicationTreeHouse.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<UserRetrieval> SignedUpUser()
        {
            // Get User Details from MongoDB(mLab) - Database
            string connectionString = "mongodb://kcs:qwerty12345@ds229458.mlab.com:29458/treehouse"; // Connection string
            var mongoClient = new MongoClient(connectionString); // New instance
            var db = mongoClient.GetDatabase("treehouse"); // Database name
            var collection = db.GetCollection<BsonDocument>("userSignUpData"); // Table name

            //var filter = Builders<BsonDocument>.Filter.Exists("Time Stamp");
            //var sort = Builders<BsonDocument>.Sort.Descending("Time Stamp");
            //var document = collection.Find(filter).Sort(sort).First();
            var docs = collection.Find(new BsonDocument()).ToListAsync().GetAwaiter().GetResult();
            List<string> myCollection = new List<string>();
            foreach (BsonDocument l in docs)
            {
                var documnt = new BsonDocument(l);

                //string id = documnt[0].ToString();
                string Name = documnt[1].ToString();
                string Email = documnt[2].ToString();
                string DateTime = documnt[3].ToString();

                //myCollection.Add(id);
                myCollection.Add(Name);
                myCollection.Add(Email);
                myCollection.Add(DateTime);
            }

            // Descending (Order by date)
            myCollection.Reverse();
            
            // max - total number of rows in the table
            int max = myCollection.Count / 3;
            int count = 0;
            
            // Limit the display range on front-end
            return Enumerable.Range(1, max).Select(index => new UserRetrieval
            {
                //Id = myCollection[count++],
                DateTime = myCollection[count++],
                Email = myCollection[count++],
                Name = myCollection[count++]
            });
        }
    }
}