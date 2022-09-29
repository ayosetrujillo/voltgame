using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    public DialogBasic dialog;
    public GameObject canvas;

    private void Start()
    {
       
    }

    private void Update()
    {
      if(dialog.dialogIsComplete)
        {
            StartCoroutine("Ending");
        }   
    }


    IEnumerator Ending()
    {
        yield return new WaitForSeconds(1f);
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }
}
