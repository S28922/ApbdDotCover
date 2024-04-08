using System;

namespace LegacyApp
{
    public interface IUserCreditService
    {
        int GetCreditLimit(string lastName, DateTime dateOfBirth);
    }

    public interface IUserCreditServiceFactory
    {
        IUserCreditService Create();
    }

    public class UserCreditService : IUserCreditService, IDisposable
    {
        private readonly Random _random = new Random();

        private readonly System.Collections.Generic.Dictionary<string, int> _database =
            new System.Collections.Generic.Dictionary<string, int>()
            {
                {"Kowalski", 200},
                {"Malewski", 20000},
                {"Smith", 10000},
                {"Doe", 3000},
                {"Kwiatkowski", 1000}
            };
        
        public int GetCreditLimit(string lastName, DateTime dateOfBirth)
        {
            int randomWaitingTime = _random.Next(3000);
            System.Threading.Thread.Sleep(randomWaitingTime);

            if (_database.ContainsKey(lastName))
                return _database[lastName];

            throw new ArgumentException($"Client {lastName} does not exist");
        }

        public void Dispose()
        {
            //Simulating disposing of resources
        }
    }

    public class UserCreditServiceFactory : IUserCreditServiceFactory
    {
        public IUserCreditService Create()
        {
            return new UserCreditService();
        }
    }
}