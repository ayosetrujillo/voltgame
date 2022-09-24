using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    [SerializeField] private GameObject _dialog;
    public bool dialogComplete = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            if (dialogComplete == false)
            {
                _dialog.SetActive(true);
            }
               
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogComplete = true;
        }
    }

}
