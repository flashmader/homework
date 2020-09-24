resource "aws_s3_bucket" "homework_s3" {
  bucket = "${var.s3-bucket-name}"
  acl    = "private"
}