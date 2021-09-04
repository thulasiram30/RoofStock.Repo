# RoofStock.Repo

This repo has two projects. For time constraint, this projects ignores unit tests
  1. Angular UI
  2. NetCore 5 web API
  
RoofStock.UI 
    1. Install npm packages
    2. Update "serviceBaseUrl" in environment file to local host api endpoint
    3. Update local host api endpoint in ClientApp\proxy.config.json as well
    4. ng serve
    
RoofStock.API
    1. update sql connection string for "RoofStockDB"key in appsettings. 
    2. Update crosOriginPolicy urls in startup file based on UI endpoint
    
    
      
  
