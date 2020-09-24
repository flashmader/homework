using System.Collections.Generic;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;

namespace PasswordHashing 
{
    public class SourceFilesS3Reader : ISourceFilesReader
    {
        private readonly AmazonS3Client _client;
        private readonly string _bucketName;
        private readonly string _path;

        public SourceFilesS3Reader(AmazonS3Client client, string bucketName, string path)
        {
            _client = client;
            _bucketName = bucketName;
            _path = path;
        }

        public async IAsyncEnumerable<SourceFile> GetFiles()
        {
            var request = new ListObjectsRequest { BucketName = _bucketName, Prefix = _path };
            var response = await _client.ListObjectsAsync(request);

            var objectKeys = response
                .S3Objects
                .Where(s3Obj =>
                {
                    var fileNameWithoutPath = s3Obj.Key.Substring(_path.Length + 1);
                    return !fileNameWithoutPath.Contains("/");
                })
                .Select(x => x.Key);

            foreach (var objectKey in objectKeys)
            {
                var csv = await _client.GetObjectAsync(new GetObjectRequest { BucketName = _bucketName, Key = objectKey });
                yield return new SourceFile(csv.ResponseStream, objectKey);
            }
        }
    }
}