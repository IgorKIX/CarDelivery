using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Threading.Tasks;

public class CloudDataHandler
{
    private string url = "";

    private APIMethods apiMethods = new APIMethods();

    private IConverter converter = new JsonConverter();

    public CloudDataHandler(string url)
    {
        this.url = url;
    }

    public async Task<Dictionary<string, string>> Load()
    {
        Response response = await apiMethods.HttpGet(url);
        if (response.Data != null)
        {
            return converter.DeserializeObject(response.Data);
        }

        return null;
    }

    public async void Save(Dictionary<string, string> data)
    {
        await apiMethods.HttpPost(url, converter.SerializeObject(data));
    }
}
