@echo off
REM Build and publish the application
echo Building and publishing application...
dotnet publish -c Release -o publish
if %ERRORLEVEL% neq 0 (
    echo Build failed!
    exit /b %ERRORLEVEL%
)

echo Creating deployment package...
tar -a -c -f publish.zip publish 
if %ERRORLEVEL% neq 0 (
    echo Package creation failed!
    exit /b %ERRORLEVEL%
)

REM Set variables
set DEPLOY_SC_PATH=publish.zip
set RESOURCE_GROUP=learn-c77d69bb-dae5-408f-8db7-685c17f0cbcd
set WEBAPP_NAME=webnaycuachien
set LOCATION="Southeast Asia"
set APP_SERVICE_PLAN=asp-webnaycuachien
set SERVICE_PLAN_PRICE=B1
set RUNTIME=DOTNETCORE|8.0

set SQL_SERVER_NAME=opitsqlserver
set SQL_ADMIN_USER=sqladmin
set SQL_ADMIN_PASSWORD=#iojfoifmkr
set SQL_DB=opitdb

echo Creating resource group...
az group create --name %RESOURCE_GROUP% --location %LOCATION%
if %ERRORLEVEL% neq 0 (
    echo Resource group creation failed!
    exit /b %ERRORLEVEL%
)

echo Creating App Service Plan...
az appservice plan create ^
    --name %APP_SERVICE_PLAN% ^
    --resource-group %RESOURCE_GROUP% ^
    --location %LOCATION% ^
    --sku %SERVICE_PLAN_PRICE% ^
    --is-linux
if %ERRORLEVEL% neq 0 (
    echo App Service Plan creation failed!
    exit /b %ERRORLEVEL%
)

echo Creating web app...
az webapp create ^
    --name %WEBAPP_NAME% ^
    --resource-group %RESOURCE_GROUP% ^
    --plan %APP_SERVICE_PLAN% ^
    --runtime %RUNTIME%
if %ERRORLEVEL% neq 0 (
    echo Web app creation failed!
    exit /b %ERRORLEVEL%
)

echo Creating SQL Server...
az sql server create ^
    --name %SQL_SERVER_NAME% ^
    --resource-group %RESOURCE_GROUP% ^
    --location %LOCATION% ^
    --admin-user %SQL_ADMIN_USER% ^
    --admin-password %SQL_ADMIN_PASSWORD%
if %ERRORLEVEL% neq 0 (
    echo SQL Server creation failed!
    exit /b %ERRORLEVEL%
)

echo Creating SQL Database...
az sql db create ^
    --resource-group %RESOURCE_GROUP% ^
    --server %SQL_SERVER_NAME% ^
    --name %SQL_DB% ^
    --service-objective "S0"
if %ERRORLEVEL% neq 0 (
    echo SQL Database creation failed!
    exit /b %ERRORLEVEL%
)

echo Setting connection string...
az webapp config connection-string set ^
    --name %WEBAPP_NAME% ^
    --resource-group %RESOURCE_GROUP% ^
    --settings "DefaultConnection=Server=tcp:%SQL_SERVER_NAME%.database.windows.net,1433;Initial Catalog=%SQL_DB%;Persist Security Info=False;User ID=%SQL_ADMIN_USER%;Password=%SQL_ADMIN_PASSWORD%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" ^
    --connection-string-type "SQLAzure"
if %ERRORLEVEL% neq 0 (
    echo Connection string setup failed!
    exit /b %ERRORLEVEL%
)

echo Deploying web app...
az webapp deployment source config-zip --resource-group %RESOURCE_GROUP% --name %WEBAPP_NAME% --src %DEPLOY_SC_PATH%
if %ERRORLEVEL% neq 0 (
    echo Deployment failed!
    exit /b %ERRORLEVEL%
)

REM Clean up
echo Cleaning up...
rmdir /s /q publish

REM Output URL and open in browser
echo Web app deployed successfully. You can access it at:
echo https://%WEBAPP_NAME%.azurewebsites.net
start https://%WEBAPP_NAME%.azurewebsites.net