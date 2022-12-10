# User API Dev Guide

Created API points 
GET - /api/Users - To Get All the users 
GET - /api/Users/{id} - To Get Particuar User Details
POST - /api/Users/Create - To Create Users

GET - /api/Accounts/{userId} - To get particular account details
GET - /api/Accounts/{recordsPerPage}/{pageNo} - Get all records page by page
GET - /api/Accounts/ - Create Accounts

## Building 

dotnet build

## Testing

dotnet test

## Deploying

dotnet restore
dotnet build
dotnet publish



## Additional Information