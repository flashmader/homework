terraform {
  backend "s3" {
    bucket = "homework.terraform.state"
    key    = "state-db"
    region = "eu-west-1"
    profile = "homework_terraform"
  }
}