using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFielName = "";

    private IConverter converter = new JsonConverter();

    public FileDataHandler(string dataDirPath, string dataFielName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFielName = dataFielName;
    }

    public Dictionary<string, string> Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFielName);
        Dictionary<string, string> loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = converter.DeserializeObject(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e + "\n");
            }
        }
        return loadedData;

    }

    public void Save(Dictionary<string, string> data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFielName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = converter.SerializeObject(data);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + e + "\n");
        }
    }
}
