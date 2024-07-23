using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Azure.Data.Tables;
using FunctionsSample.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionsSample
{
    public class FunBlobUpload
    {
        private readonly ILogger<FunBlobUpload> _logger;

        public FunBlobUpload(ILogger<FunBlobUpload> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunBlobUpload))]
        public async Task Run([BlobTrigger("democontainer/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            await TableRowEntry(name);
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }

        private async Task TableRowEntry(string name)
        {
            var serviceClient = new TableServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));

            TableClient table = serviceClient.GetTableClient("clientdocuments");
            table.CreateIfNotExists();

            //added this line
            CreateMessage(table, new ClientFileModel($"New file uploaded {name}", DateTime.UtcNow));
            
        }

        static void CreateMessage(TableClient table, ClientFileModel message)
        {
            table.AddEntity(message);
        }
    }
}
