using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConverter
{
    string SerializeObject(Dictionary<string, string> data);
    Dictionary<string, string> DeserializeObject(string data);
}
