using System.Collections.Generic;
using MongoDB.Driver;
using DotnetAPI.Models;

namespace DotnetAPI.Services
{
    public class MusicService
    {
        private readonly IMongoCollection<Music> _music;

        public MusicService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _music = database.GetCollection<Music>("music");
        }

        public Music Create(Music music)
        {
            _music.InsertOne(music);
            return music;
        }

        public IList<Music> Read() => _music.Find(mus => true).ToList();
        public Music Find(string id) => _music.Find(mus => mus.Id == id).SingleOrDefault();
        public void Update(Music music) => _music.ReplaceOne(mus=>mus.Id == music.Id, music);
        public void Delete(string id) => _music.DeleteOne(mus=>mus.Id == id);
    }
}