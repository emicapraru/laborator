using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace L04
{
    class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        static void Main(string[] args)
        {
            Task.Run(async()=>{await Initialize();})
            .GetAwaiter()
            .GetResult();
        }

        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;" + "AccountName=capraruemil3datc" + ";AccountKey=ETJt6Wi3MRfnbAjDbZYnHvLd8rd6qP74Vyz0lrUZiEA7wo90vtjPHioGmi0REQq/hyT+rp6PjPYf1E4uJzDJRw==" + ";EndpointSuffix=core.windows.net";
            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();
            studentsTable = tableClient.GetTableReference("studenti");
            await studentsTable.CreateIfNotExistsAsync();   
            //await AddNewStudent();
            await GetAllStudents();
            
        }
        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tNume\tEmail\tNumar telefon\tAn");
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>();

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;

                foreach (StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}", entity.PartitionKey , entity.RowKey,
                    entity.FirstName, entity.LastName,
                    entity.Email, entity.PhoneNumber,
                    entity.Year);
                }
            }while (token != null);
        }

        private static async Task AddNewStudent()
        {
            var student = new StudentEntity ("UPT","1990104350016");
            student.FirstName = "Emi";
            student.LastName = "Capraru";
            student.Email = "emilcapraru4@gmail.com";
            student.Year = 4;
            student.PhoneNumber = "0773886102";
            student.Faculty= "AC";

            var insertOperation = TableOperation.Insert(student);
            await studentsTable.ExecuteAsync(insertOperation);
        }
    }
}