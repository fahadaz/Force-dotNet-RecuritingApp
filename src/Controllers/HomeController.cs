using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;
using Salesforce.Common;
using Salesforce.Force;

namespace rapp.Controllers
{
    public class HomeController : Controller
    {
               
        private AppSettings _appSetting;
        
        public HomeController(IOptions<AppSettings> appSettings)
        {
            this._appSetting = appSettings.Options;
        }
        
        private async Task<ForceClient> GetForceClient()
        {
            var auth = new AuthenticationClient();
            var url = _appSetting.IsSandboxUser.Equals("true", StringComparison.CurrentCultureIgnoreCase)
                ? _appSetting.ProdUrl : _appSetting.SandboxUrl;

            await auth.UsernamePasswordAsync(_appSetting.ConsumerKey, _appSetting.ConsumerSecret, _appSetting.Username, _appSetting.Password, url);
            var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
            return client;
        }
        public async Task<IActionResult> Index()
        {
            var client = await GetForceClient();
            
            const string qry = "select Name, Job_Description__c, Location__c, Functional_Area__c from Position__c where Status__c like 'Open%' ";
            var pos = new List<Position>();
            var results = await client.QueryAsync<Position>(qry);
            var totalSize = results.TotalSize;
            
            ViewBag.Size = totalSize;
            pos.AddRange(results.Records);
            var nextRecUrl = results.NextRecordsUrl;
            
            if(!string.IsNullOrEmpty(nextRecUrl))
            {
                while(true)
                {
                    var contResults = await client.QueryContinuationAsync<Position>(nextRecUrl);
                    totalSize = contResults.TotalSize;
                    
                    pos.AddRange(contResults.Records);
                    if(string.IsNullOrEmpty(contResults.NextRecordsUrl)) break;
                    
                    nextRecUrl = contResults.NextRecordsUrl;                    
                }
            }
            
            ViewBag.Pos = pos;
                        
            return View();
        }

        public async Task<IActionResult> Detail(string name)
        {
            var client = await GetForceClient();
            const string qry = "select Name,Job_Description__c, Functional_Area__c, Location__c, Skills_Required__c, Responsibilities__c, Educational_Requirements__c from Position__c where Name = '{0}'";
            
            var pos = new List<Position>();
            var results = await client.QueryAsync<Position>(string.Format(qry, name));                
            var totalSize = results.TotalSize;
            pos.AddRange(results.Records);
            var nextRecUrl = results.NextRecordsUrl;
            
            if(!string.IsNullOrEmpty(nextRecUrl))
            {
                while(true)
                {
                    var contResults = await client.QueryContinuationAsync<Position>(nextRecUrl);
                    totalSize = contResults.TotalSize;
                    
                    pos.AddRange(contResults.Records);
                    if(string.IsNullOrEmpty(contResults.NextRecordsUrl)) break;
                    
                    nextRecUrl = contResults.NextRecordsUrl;                    
                }
            }
            
            if(pos.Count() > 0)
                ViewBag.Position = pos.FirstOrDefault();
            
            return View();
        }
        
        public async Task<IActionResult> Apply(string name)
        {
            var client = await GetForceClient();
            const string qry = "select Name,Job_Description__c, Functional_Area__c, Location__c from Position__c where Name = '{0}'";
            
            var pos = new List<Position>();
            var results = await client.QueryAsync<Position>(string.Format(qry, name));
            var totalSize = results.TotalSize;
            pos.AddRange(results.Records);
            var nextRecUrl = results.NextRecordsUrl;
            
            if(!string.IsNullOrEmpty(nextRecUrl))
            {
                while(true)
                {
                    var contResults = await client.QueryContinuationAsync<Position>(nextRecUrl);
                    totalSize = contResults.TotalSize;
                    
                    pos.AddRange(contResults.Records);
                    
                    if(string.IsNullOrEmpty(contResults.NextRecordsUrl)) break;
                    
                    nextRecUrl = contResults.NextRecordsUrl;                    
                }
            }
            
            if(pos.Count() > 0)
                ViewBag.Position = pos.FirstOrDefault();
            
            return View();
        }
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
    
    public class Position
        {
            public const String SObjectTypeName = "Position";
            public String Id { get; set; }
            public String Name { get; set; }
            public string Job_Description__c { get; set; }
            public string Location__c { get; set; }
            public string Functional_Area__c { get; set; }            
            public string  Skills_Required__c {get;set;}
            public string  Responsibilities__c{get;set;}
            public string Educational_Requirements__c{get;set;}            
        }
}
