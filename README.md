# Force-dotNet-RecuritingApp
* asp.net vnext integration sample application with a custom force.com app 
  * This application uses asp.net vnext beta 7 and heroku build pack
  * Heroku Build pack https://github.com/heroku/dotnet-buildpack
* This application is posted under Apache License.
* This application uses source code from https://github.com/developerforce/Force.com-Toolkit-for-NET

--------------------------------------------------------------------------------------
Following are the steps to get this application working on heroku

* Need to first install salesforce application using following url 
  https://login.salesforce.com/packaging/installPackage.apexp?p0=04t1a000000EZ6u
* Create connected app "rapp" under Setup | Create | Apps | Connected Apps. Use information under API section in next step. 
* Download the source from this git repo on to your local machine
  https://github.com/fahadaz/Force-dotNet-RecuritingApp
* Open ~/<applicaltion folder>/src/config.json and add values from previous step and save
```  
{
  "AppSettings": {
    "SiteTitle": "rapp",
    "ConsumerKey":"",
    "ConsumerSecret":"",
    "Username":"",
    "Password":"",    
    "IsSandboxUser":"false",
    "ProdUrl":"https://test.salesforce.com/services/oauth2/token",
    "SandboxUrl":"https://login.salesforce.com/services/oauth2/token"
  }
}
```
* Use heroku build pack https://github.com/heroku/dotnet-buildpack to create a new heroku application.

Thats it!
