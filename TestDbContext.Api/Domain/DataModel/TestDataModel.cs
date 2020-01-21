using System;

namespace TestDbContext.Domain.DataModel
{
    public class TestDataModel
    {
        public TestDataModel(string name, string property, DateTimeOffset timestamp)
        {
            Name = name;
            Property = property;
            Timestamp = timestamp;
        }

        public string Name { get; set; }
        public string Property { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
