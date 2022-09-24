using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBasic : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.02f;
    public GameObject trigger;

    private Animator _animatorController;
    private int _index;

    private void Awake()
    {
        _animatorController = GetComponent<Animator>();
    }

    void Start()
    {
        textComponent.text = string.Empty;
        PlayerController.instance.playerCanMove = false;
        PlayerController.instance.playerIsMoving = false;

        StartDialogue();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[_index])
            {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[_index];
            }
        }         
    }


    void StartDialogue()
    {

        _index = 0;
        StartCoroutine(TypeLine());
    }


    void NextLine()
    {
        if(_index < lines.Length - 1)
        {
            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            
        } else {
            StartCoroutine("CloseDialog");
            //Erase the trigger dialog.
            trigger.SetActive(false);
        }
    }


    IEnumerator TypeLine()
    {
        //type each character 1 by 1
        foreach (char character in lines[_index].ToCharArray())
        {
            textComponent.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    IEnumerator CloseDialog()
    {
        _animatorController.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        PlayerController.instance.playerCanMove = true;
        gameObject.SetActive(false);
    }

}
