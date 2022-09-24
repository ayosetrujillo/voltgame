using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBasic : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.02f;

    private Animator _animatorController;
    private int _index;
    private GameObject _player;

    private void Awake()
    {
        _animatorController = GetComponent<Animator>();
    }

    void Start()
    {
        textComponent.text = string.Empty;

        StartDialogue();
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            PlayerController.instance.playerCanMove = false;
            PlayerController.instance.playerIsMoving = false;
        }

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
        AudioManagerController.instance.PlaySFX(23);
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
