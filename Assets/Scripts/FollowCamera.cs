using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Camera(this) should follow the car
    [SerializeField] private GameObject targetToFollow;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetToFollow.transform.position + new Vector3(0,0,-10);
    }
}
