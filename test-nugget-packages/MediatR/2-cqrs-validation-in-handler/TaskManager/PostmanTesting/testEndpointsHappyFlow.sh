#!/bin/bash
#   Test the happy flow of all the endpoints

newman run -k TaskManager.CreateTask.postman_collection.json   -e CreateTask-HappyFlow.postman_environment.json
newman run -k TaskManager.GetTask.postman_collection.json      -e GetTask-HappyFlow.postman_environment.json