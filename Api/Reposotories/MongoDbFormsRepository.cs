using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Reposotories
{
    public class MongoDbFormsRepository : IFormRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "forms";
        private readonly IMongoCollection<Form> formsCollection;
        private readonly FilterDefinitionBuilder<Form> filterBuilder = Builders<Form>.Filter;
        public MongoDbFormsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            formsCollection = database.GetCollection<Form>(collectionName);
        }
        /// <summary>
        /// Takes values from Post request and stores it in database
        /// </summary>
        /// <param name="form"></param>
        /// <returns>InsertOneAsync</returns>
        public async Task CreateForm(Form form)
        {
            await formsCollection.InsertOneAsync(form);
        }
        /// <summary>
        /// Delete Json string from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DeleteOneAsync</returns>
        public async Task DeleteForm(Guid id)
        {
            var filter = filterBuilder.Eq(form => form.Id, id);
            await formsCollection.DeleteOneAsync(filter);
        }
        /// <summary>
        /// get request
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SingleOrDefaultAsync</returns>
        public async Task<Form> GetForm(Guid id)
        {
            var filter = filterBuilder.Eq(form => form.Id, id);
            return await formsCollection.Find(filter).SingleOrDefaultAsync();
        }
        /// <summary>
        /// get request
        /// </summary>
        /// <returns>ToListAsync</returns>
        public async Task<IEnumerable<Form>> GetForms()
        {
            return await formsCollection.Find(new BsonDocument()).ToListAsync();
        }
        /// <summary>
        /// search for lärare
        /// </summary>
        /// <param name="lärare"></param>
        /// <returns>if one or many lärare found</returns>
        public async Task<IEnumerable<Form>> Search(string lärare)
        {
            var filter = filterBuilder.Eq(form => form.Lärare, lärare);
            
            return await formsCollection.Find(filter).ToListAsync();
        }
        /// <summary>
        /// updates form with new values
        /// </summary>
        /// <param name="form"></param>
        /// <returns>ReplaceOneAsync</returns>
        public async Task UpdateForm(Form form)
        {
            var filter = filterBuilder.Eq(exsistingForm => exsistingForm.Id, form.Id);
            await formsCollection.ReplaceOneAsync(filter, form);
        }
    }
} 
// docker run -d --rm --name mongo -p 27017:27017 -v mongodbata:/data/db -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=admin123 mongo
// docker run -d --rm --name mongo -p 27017:27017 -v mongodbata:/data/db mongo