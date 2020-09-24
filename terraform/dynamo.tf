resource "aws_dynamodb_table" "homework_dynamodb" {
  name           = "${var.dynamo-table-name}"
  read_capacity  = 20
  write_capacity = 20
  hash_key       = "Path"
  range_key      = "ID"

  attribute {
    name = "Path"
    type = "S"
  }
  attribute {
    name = "ID"
    type = "S"
  }
  attribute {
    name = "Datetime"
    type = "S"
  }

  local_secondary_index {
    name               = "DatetimeIndex"
    range_key          = "Datetime"
    projection_type    = "ALL"
    # projection_type    = "INCLUDE"
    # non_key_attributes = ["Hash"]
  }

  tags = {
    Name        = "homework"
  }
}