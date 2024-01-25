using CodeChallanger.UI.Interfaces;
using StackExchange.Redis;


namespace CodeChallanger.UI.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(string connectionString)
        {
            var connection = ConnectionMultiplexer.Connect(connectionString);
            _database = connection.GetDatabase();
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var serializedValue = System.Text.Json.JsonSerializer.Serialize(value);
            _database.StringSet(key, serializedValue, expiration);
        }

        public T Get<T>(string key)
        {
            var value = _database.StringGet(key);
            if (!value.HasValue)
            {
                return default;
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
