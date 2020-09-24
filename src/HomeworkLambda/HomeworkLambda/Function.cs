using Amazon.Lambda.Core;
using Amazon.Runtime;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HomeworkLambda
{
    public class Function
    {
        public string FunctionHandler(string folderPath, ILambdaContext context)
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
