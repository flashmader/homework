data "aws_iam_role" "lambda_execution_role" {
  provider = "aws"

  name = "homework-lambdaexecution-role"
}

# TODO: requires tricks with uploading lambda code to S3

# resource "aws_lambda_function" "lambda_homework" {
#   provider = "aws"
#   function_name = "lambda_homework"
# 
#   role = "${data.aws_iam_role.lambda_execution_role.arn}"
#   runtime = "dotnetcore3.1"
#   handler = "HomeworkLambda::HomeworkLambda.Function::FunctionHandler"
# 
#   environment {
#     variables = {
#         "S3BucketName" = "${var.s3-bucket-name}", 
#         "DynamoTableName" = "${var.dynamo-table-name}",
#     }
#   }
# }