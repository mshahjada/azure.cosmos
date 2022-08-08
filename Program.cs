// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.Cosmos;


string endpoint = "https://ms-cosmos-testdb.documents.azure.com:443/";
string primarykey = "MP9wvAcTQVNSe4uWUtBdUQ3jloFOPQY1VPopLH3mxkYTTGB10H0I1RQsbwAHkqMa68xLkPKgV3Wo1FEB78FYzg==";

Console.WriteLine("Connect Cosmos Client");

CosmosClient  cosmosClient = new CosmosClient(endpoint, primarykey);

var container =  cosmosClient.GetContainer("DB01", "C01");

var query = container.GetItemQueryIterator<object>(
    new QueryDefinition("SELECT * FROM c"), 
    requestOptions: new QueryRequestOptions(){
        MaxConcurrency= 1
    });

var results = new List<object>();

while(query.HasMoreResults){
    var feeds = await query.ReadNextAsync();
    //feedtask.Wait();

    results.AddRange(feeds.ToList());
}

Console.WriteLine($"{results.Count()}");


Console.ReadKey();

