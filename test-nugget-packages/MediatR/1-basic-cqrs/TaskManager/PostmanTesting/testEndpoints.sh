#!/bin/bash
#   Please Execute this after the startEndpoints.sh is running

echo "###  CreateTask - Testing Happy Flow ###"
newman run --verbose -k CreateTask.postman_collection.json   -e CreateTask-HappyFlow.postman_environment.json

echo "###  GetTaskQuery - Testing Happy Flow ###"
newman run --verbose -k GetTaskQuery.postman_collection.json   -e GetTaskQuery-HappyFlow.postman_environment.json
