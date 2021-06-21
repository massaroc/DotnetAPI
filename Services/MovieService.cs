using System.Collections.Generic;
using MongoDB.Driver;
using DotnetAPI.Models;

namespace DotnetAPI.Services
{
    public class MovieService
    {
        private readonly IMongoCollection<Movie> _movies;

        public MovieService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _movies = database.GetCollection<Movie>("movies");
        }

        public Movie Create(Movie movie)
        {
            _movies.InsertOne(movie);
            return movie;
        }

        public IList<Movie> Read() => 
            _movies.Find(mov => true).ToList();

        public Movie Find(string id) =>
            _movies.Find(mov=>mov.Id == id).SingleOrDefault();

        public void Update(Movie movie) =>
            _movies.ReplaceOne(mov=>mov.Id == movie.Id, movie);
        
        public void Delete(string id) =>
            _movies.DeleteOne(mov=>mov.Id == id);

    }
}