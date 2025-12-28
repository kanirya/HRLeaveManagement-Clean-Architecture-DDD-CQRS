provider "aws" {
  region                      = "us-east-1"
  access_key                  = "test"
  secret_key                  = "test"
  skip_credentials_validation = true
  skip_metadata_api_check     = true

  endpoints {
    logs = "http://localstack:4566"
  }
}

resource "aws_cloudwatch_log_group" "api" {
  name              = "/aspire/api"
  retention_in_days = 7
}
