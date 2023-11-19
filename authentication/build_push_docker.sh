#! /usr/bin/sh

docker compose build
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 598490276344.dkr.ecr.us-east-1.amazonaws.com
docker tag authentication-api:latest 598490276344.dkr.ecr.us-east-1.amazonaws.com/bcit-local:authentication-api
docker push 598490276344.dkr.ecr.us-east-1.amazonaws.com/bcit-local:authentication-api