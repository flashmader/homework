using System;

namespace PasswordHashing 
{
    public class SourceRecord
    {
        public SourceRecord(Guid id, string password, string name, DateTime dateTime, string path)
        {
            Id = id;
            Password = password;
            Name = name;
            DateTime = dateTime;
            Path = path;
        }

        public Guid Id { get; }
        public string Password { get; }
        public string Name { get; }
        public DateTime DateTime { get; }
        public string Path { get; }
    }
}