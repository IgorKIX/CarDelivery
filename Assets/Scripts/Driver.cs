using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] private float steerSpeed = 0.1f; 
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float slowSpeed = 15f;
    [SerializeField] private float boostSpeed = 50f;

    private float _defaultSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;// deltaTime - delta of how much frames is render per second
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boost")
        {
            moveSpeed = boostSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        moveSpeed = slowSpeed;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boost")
        {
            Debug.Log("Boost!");
            moveSpeed = _defaultSpeed;
        }
    }
}
