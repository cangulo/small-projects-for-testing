#!/bin/bash
#   Please Execute this after the startEndpoints.sh is running

newman run -k TaskManager.CreateTask.postman_collection.json   -e CreateTask-HappyFlow.postman_environment.json
newman run -k TaskManager.GetTask.postman_collection.json      -e GetTask-HappyFlow.postman_environment.json