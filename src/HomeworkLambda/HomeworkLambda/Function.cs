using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Newtonsoft.Json.Linq;
using PasswordHashing;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace HomeworkLambda
{
    public class Function
    {
        public async Task<string> FunctionHandler(JObject input, ILambdaContext context)
        {
            try
            {
                var path = input["path"];
                if (path == null)
                {
                    return "Path must be specified";
                }

                var bucketName = Environment.GetEnvironmentVariable("S3BucketName");
                var dynamoTableName = Environment.GetEnvironmentVariable("DynamoTableName");

                var hashingService = new PasswordHashingService(FallbackCredentialsFactory.GetCredentials(), bucketName, dynamoTableName);
                await hashingService.Execute(path.ToString());
                
                return "Ok";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}
