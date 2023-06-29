using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonConverter : IConverter
{
    public string SerializeObject(Dictionary<string, string> data)
    {
        return JsonConvert.SerializeObject(data);
    }
    public Dictionary<string, string> DeserializeObject(string data)
    {
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
    }
}
