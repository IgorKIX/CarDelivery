using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagesCountText : MonoBehaviour
{
    public int PackagesCount = 0;

    public Text PackagesOutput;

    private void Update()
    {
        PackagesOutput.text = "Delivered: " + PackagesCount;
    }

    private void OnPackageDelivery()
    {
        PackagesCount++;
    }
}
