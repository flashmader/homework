using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace PasswordHashing 
{
    public class DynamoTargetWriter : ITargetWriter
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly string _tableName;

        public DynamoTargetWriter(AmazonDynamoDBClient client, string tableName)
        {
            _client = client;
            _tableName = tableName;
        }

        public async Task Write(IEnumerable<SourceRecord> values)
        {
            BatchWriteItemRequest request = new BatchWriteItemRequest();
            List<WriteRequest> writeRequests = new List<WriteRequest>();

            foreach (var sourceRecord in values)
            {
                var dynamoItem = MapSourceRecordToDynamoAttributes(sourceRecord);
                WriteRequest writeRequest = new WriteRequest(new PutRequest(dynamoItem));
                writeRequests.Add(writeRequest);
            }

            request.RequestItems = new Dictionary<string, List<WriteRequest>>
            {
                {
                    _tableName, 
                    writeRequests
                }
            };
            
            await _client.BatchWriteItemAsync(request);
        }

        private static Dictionary<string, AttributeValue> MapSourceRecordToDynamoAttributes(SourceRecord sourceRecord)
        {
            var passwordHash = Md5Helper.ComputeHash(sourceRecord.Password);
            var dynamoItem = new Dictionary<string, AttributeValue>
            {
                {"Path", new AttributeValue {S = sourceRecord.Path}},
                {"ID", new AttributeValue {S = sourceRecord.Id.ToString()}},
                {"Datetime", new AttributeValue {S = sourceRecord.DateTime.ToString()}},
                {"Hash", new AttributeValue {S = passwordHash}}
            };
            return dynamoItem;
        }
    }
}