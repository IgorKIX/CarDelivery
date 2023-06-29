using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    void LoadData(Dictionary<string, string> data);
    void SaveData(Dictionary<string, string> data);
}
