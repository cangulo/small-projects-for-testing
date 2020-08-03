#!/bin/bash
#   Please Execute this after the startEndpoints.sh is running

echo "###  CreateTask - Testing Happy Flow ###"
newman run --verbose -k CreateTask.postman_collection.json   -e CreateTask-HappyFlow.postman_environment.json

echo "###  CreateTask - Testing Validations###"
newman run --verbose -k CreateTask.postman_collection.json   -e CreateTask-Error-Title-Empty.postman_environment.json
newman run --verbose -k CreateTask.postman_collection.json   -e CreateTask-Error-Title-TooLong.postman_environment.json
newman run --verbose -k CreateTask.postman_collection.json   -e CreateTask-Error-TodoDate-Past.postman_environment.json
newman run --verbose -k CreateTask.postman_collection.json   -e CreateTask-Error-TitleEmpty-and-TodoDate-Past.postman_environment.json

echo "###  GetTaskQuery - Testing Happy Flow ###"
newman run --verbose -k GetTaskQuery.postman_collection.json   -e GetTaskQuery-HappyFlow.postman_environment.json

echo "###  GetTaskQuery- Testing Validations###"
newman run --verbose -k GetTaskQuery.postman_collection.json   -e GetTaskQuery-Error-ClientIdInvalid.postman_environment.json