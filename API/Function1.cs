using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace API
{

    public class User
    {

        [JsonProperty("id")]
        [JsonRequired]
        public string id { get; set; }

        [JsonProperty("managerId")]
        [JsonRequired]
        public string managerId { get; set; }

        [JsonProperty("firstName")]
        [JsonRequired]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        [JsonRequired]
        public string lastName { get; set; }

        [JsonProperty("hireDate")]
        [JsonRequired]
        public string hireDate { get; set; }

        [JsonProperty("dischargeDate")]
        public string dischargeDate { get; set; }

        [JsonProperty("position")]
        [JsonRequired]
        public string position { get; set; }


    }

    public static class Function1
    {

        
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }


        [FunctionName("CosmosDb_GetTodos")]
        public static IActionResult GetTodos(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")]HttpRequest req,
        [CosmosDB(
                databaseName: "databsgyemselpupinu",
                collectionName: "userslist",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT * FROM c")]
                IEnumerable<Todo> todos, TraceWriter log)
                {
                    log.Info("Getting todo list items");
                    return new OkObjectResult(todos);
                }
        [FunctionName("CosmosDb_GetTodoById")]
        public static IActionResult GetTodoById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")]HttpRequest req,
        [CosmosDB
            (databaseName: "databsgyemselpupinu", 
            collectionName: "userslist", 
            ConnectionStringSetting = "CosmosDBConnection",
            Id = "{id}")] Todo todo,
        TraceWriter log, string id)
            {
                log.Info("Getting todo item by id");

                if (todo == null)
                {
                    log.Info($"Item {id} not found");
                    return new NotFoundResult();
                }
                return new OkObjectResult(todo);
            }

        [FunctionName("CosmosDb_Adduser")]
        public static async Task<IActionResult> AdduserAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")]HttpRequest req,
        [CosmosDB
                (databaseName: "databsgyemselpupinu",
                collectionName: "userslist",
                ConnectionStringSetting = "CosmosDBConnection")]
                IAsyncCollector<User> todos, TraceWriter log)
        {
            log.Info("Adding user");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<TodoUpdateModel>(requestBody);
            //todo check if all values exists
            log.Info(requestBody);
            User user1;
            user1 = new  User()
            {
                id = input.id,
                managerId = input.managerId,
                firstName = input.firstName,
                lastName = input.lastName,
                hireDate = input.hireDate,
                dischargeDate = input.dischargeDate,
                position = input.position
            };
                
            await todos.AddAsync(user1);
            return new OkObjectResult(todos);
            
        }
      
        


    }

}
