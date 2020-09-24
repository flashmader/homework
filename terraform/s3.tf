resource "aws_s3_bucket" "homework_s3" {
  bucket = "homework-s3"
  acl    = "private"
}