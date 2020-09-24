using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.S3;

namespace PasswordHashing 
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private readonly AWSCredentials _credentials;
        private readonly string _sourceBucketName;
        private readonly string _targetDynamoTableName;

        public PasswordHashingService(AWSCredentials credentials, string sourceBucketName, string targetDynamoTableName)
        {
            _credentials = credentials;
            _sourceBucketName = sourceBucketName;
            _targetDynamoTableName = targetDynamoTableName;
        }

        public async Task Execute(string sourcePath)
        {
            var fileParser = new FileParser();
            var allSourceRecords = new Dictionary<Guid, SourceRecord>();

            using var amazonS3Client = new AmazonS3Client(_credentials);
            ISourceFilesReader srcReader = new SourceFilesS3Reader(amazonS3Client, _sourceBucketName, sourcePath);

            await foreach (var srcFile in srcReader.GetFiles())
            {
                var srcRecords = fileParser.GetRecords(srcFile);
                foreach (var sourceRecord in srcRecords)
                {
                    if (allSourceRecords.ContainsKey(sourceRecord.Id))
                    {
                        throw new Exception($"Folder contains duplicated ID {sourceRecord.Id}");
                    }
                    allSourceRecords.Add(sourceRecord.Id, sourceRecord);
                }
            }

            using AmazonDynamoDBClient amazonDynamoDbClient = new AmazonDynamoDBClient(_credentials);
            ITargetWriter targetWriter = new DynamoTargetWriter(amazonDynamoDbClient, _targetDynamoTableName);

            await targetWriter.Write(allSourceRecords.Values);
        }
    }
}