using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//MongoDB - mLab cloud
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using Microsoft.Extensions.Configuration.Json;

namespace WebApplicationTreeHouse.Controllers
{
    [Produces("application/json")] // Value output in json
    [Route("api/User")] // API data
    public class UserController : Controller
    {
        [HttpPost("register")] // On submit button click
        public IEnumerable<Models.UserData> Register([FromBody]Models.UserData user)
        {
            // Wite User Details to MongoDB(mLab) - Database
            string connectionString = "mongodb://kcs:qwerty12345@ds229458.mlab.com:29458/treehouse"; // Connection string
            var mongoClient = new MongoClient(connectionString); // New instance
            var db = mongoClient.GetDatabase("treehouse"); // Database name
            var collection = db.GetCollection<BsonDocument>("userSignUpData"); // Table name

            // Add data to database
            var documnt = new BsonDocument
            {
                {"Full Name", user.SignupName}, // Column name 1
                {"Email", user.SignupEmail}, // Column name 2
                {"Time Stamp", DateTime.Now.ToString("g")} // DateTime stamp
            };
            collection.InsertOneAsync(documnt); // Save values in the table

            return new Models.UserData[]
            {
                // Get data on 'api/User'
                new Models.UserData { SignupName = "user.SignupName", SignupEmail = "user.SignupEmail" }
            };
        }
    }
}