using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsSample.Models
{
    public class ClientFileModel: ITableEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public ClientFileModel(string name, DateTime date)
        {
            Name = name;
            Date = date;
            PartitionKey = "ThanksApp";
            RowKey = name;
        }

        public ClientFileModel()
        {

        }
    }
}
