using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    public GameManager GM;
    [SerializeField] private float destroyDelayTime = 0.1f;
    [SerializeField] private Color32 packageColor = new Color32(1, 1, 1, 1);
    [SerializeField] private Color32 defaultCarColor = new Color32(1, 1, 1, 1);
    private bool _hasPackage = false;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultCarColor = _spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("BUM!");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Package" && !_hasPackage)
        {
            Debug.Log("You've got a package!");
            _spriteRenderer.color = packageColor;
            Destroy(col.gameObject, destroyDelayTime);
            _hasPackage = true;
        }

        if (col.tag == "Customer" && _hasPackage)
        {
            Debug.Log("Delivered package!");
            GM.OnPackageDelivery();
            _spriteRenderer.color = defaultCarColor;
            _hasPackage = false;
        }
    }
}
