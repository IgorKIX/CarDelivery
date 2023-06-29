using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public int PackagesCount = 0;

    public Text PackagesOutput;

    private void Update()
    {
        PackagesOutput.text = "Delivered: " + PackagesCount;
    }

    public void OnPackageDelivery()
    {
        PackagesCount++;
    }

    public void LoadData(Dictionary<string, string> data)
    {
        if (data.ContainsKey(nameof(this.PackagesCount)))
        {
            try
            {
                int numVal = Convert.ToInt32(data[nameof(this.PackagesCount)]);
                this.PackagesCount = numVal;
            }
            catch (FormatException)
            {
                Debug.LogError("Error occured when trying to convert string to int \n");
            }
        }
    }

    public void SaveData(Dictionary<string, string> data)
    {
        data[nameof(this.PackagesCount)] = this.PackagesCount.ToString();
    }
}
