using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Microsoft.Azure.Cosmos.Table;
//using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using System.Linq;

public class AzureManager : MonoBehaviour
{
    public class Profile {
        public string Email;
        public string ProfileName;
        public int Score;

        public Profile(string email, string profileName, int score){
            Email = email;
            ProfileName = profileName;
            Score = score;
            //RowKey = Email;
            //PartitionKey = ProfileName;

        }
    }
    string connectionString = "DefaultEndpointsProtocol=https;AccountName=rhythmtable;AccountKey=h71xLBMIlwJFL1LQfaiQuVt8yGhllsO1vgF6tiBjK8RMDABiawxfqiFnwD5bhJdHZhzzHIjEGd38kQHfgIB7cw==;TableEndpoint=https://rhythmtable.table.cosmos.azure.com:443/;";
    //StorageCredentials storageCredentials = new StorageCredentials("rhythmtable", "h71xLBMIlwJFL1LQfaiQuVt8yGhllsO1vgF6tiBjK8RMDABiawxfqiFnwD5bhJdHZhzzHIjEGd38kQHfgIB7cw==");
    //CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("rhythmtable", "h71xLBMIlwJFL1LQfaiQuVt8yGhllsO1vgF6tiBjK8RMDABiawxfqiFnwD5bhJdHZhzzHIjEGd38kQHfgIB7cw=="), useHttps : true);
    //System.Uri uri = new System.Uri("https://rhythmtable.table.cosmos.azure.com:443/");
    //CloudTableClient tableClient = new CloudTableClient(new System.Uri("https://rhythmtable.table.cosmos.azure.com:443/"), new StorageCredentials("rhythmtable", "h71xLBMIlwJFL1LQfaiQuVt8yGhllsO1vgF6tiBjK8RMDABiawxfqiFnwD5bhJdHZhzzHIjEGd38kQHfgIB7cw=="));
    //CloudTable table = new CloudTableClient(new System.Uri("https://rhythmtable.table.cosmos.azure.com:443/"), new StorageCredentials("rhythmtable", "h71xLBMIlwJFL1LQfaiQuVt8yGhllsO1vgF6tiBjK8RMDABiawxfqiFnwD5bhJdHZhzzHIjEGd38kQHfgIB7cw==")).GetTableReference("Profiles");
    public Button buttn;
    DocumentClient client = new DocumentClient(new System.Uri("https://rhythm.documents.azure.com:443/"), "TRdCSBYcsgEpwpduzrTxVkQ7jfsQKjIN6XXnGVMuLUb4OV0XzRTxtGKt6vsEh3T5bRLhnYHcM8lIPVgqjsAEmg==");

    // public static Profile InsertOrMergeEntityAsync(CloudTable table, Profile entity)
    // {   
    //     if (entity == null)
    //     {
    //         Debug.LogWarning("entity is null");
    //     }

    //     // try
    //     // {
    //         // Create the InsertOrReplace table operation
    //         //TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

    //         // Execute the operation.
    //         //TableResult result = table.Execute(insertOrMergeOperation);
    //         Profile insertedCustomer = result.Result as Profile;

    //         //if (result.RequestCharge.HasValue)
    //         //{
    //         //    Debug.Log("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
    //         //}

    //         return insertedCustomer;
    //     // }
    //     // catch (Exception e)
    //     // {
    //     //     Debug.Log(e.Message);
    //     //     throw;
    //     // }
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool once = true;
    // Update is called once per frame
    void Update()
    {
        if(once){
            Profile p = new Profile("donkey@dunk.dk", "dinkel", 999);
            // Profile s = InsertOrMergeEntityAsync(table, p);
            //Debug.Log(s.Email);
            once = false;
            Uri uri = UriFactory.CreateDocumentCollectionUri("RhythmGame", "Profiles");
            

            //Uri uri = UriFactory.CreateDocumentCollectionUri("RhythmGame", "Profiles");
            //client.CreateDocumentAsync(uri, p);


            FeedOptions queryOptions = new FeedOptions { EnableCrossPartitionQuery = true };
            IQueryable<Profile> userQueryInSql = client.CreateDocumentQuery<Profile>(
                    uri, queryOptions).Where(x => x.ProfileName == "dinkel");
            //Console.WriteLine(userQueryInSql);
            foreach (Profile user in userQueryInSql)
            {
                Debug.Log(user.Email);
            }
        }
    }
}
