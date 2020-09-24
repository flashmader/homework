using System.Threading.Tasks;
using Amazon.Runtime;
using PasswordHashing;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var awsCredentials = new StoredProfileAWSCredentials("homework_terraform");
            const string PREFIX = "nested folder";
            const string BUCKET_NAME = "homework-s3";
            const string DYNAMO_TABLE_NAME = "homework-dynamodb";

            await new PasswordHashingService(awsCredentials, BUCKET_NAME, DYNAMO_TABLE_NAME)
                .Execute(PREFIX);
        }
    }
}
