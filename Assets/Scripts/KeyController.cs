using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private string keyName;

    public bool key1;
    public bool key2;
    public bool key3;
    public bool bossKey;

    private void Start()
    {
        if(PlayerPrefs.HasKey(keyName))
        {
            Destroy(gameObject);
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(key1) { UIController.instance.key1.SetActive(true); UIController.instance.hasKey1 = true; }
            if(key2) { UIController.instance.key2.SetActive(true); UIController.instance.hasKey2 = true; }
            if(key3) { UIController.instance.key3.SetActive(true); UIController.instance.hasKey3 = true; }

            if(bossKey) { UIController.instance.bossKey.SetActive(true); UIController.instance.hasBossKey = true; }

            PlayerPrefs.SetInt(keyName, 1);

            Destroy(gameObject);
        }
    }
}
