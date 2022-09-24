using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITitleController : MonoBehaviour
{
    public GameObject fadeFX;
    public string startGameScene;
    public string tutorialScene;
    public Animator _animatorFadeFX;
    public bool playLogoSFX = true;
    //public Animator ship;

    [SerializeField] private GameObject _continueBTN;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        FadeOut();

        if(AudioManagerController.instance.titleMusic.isPlaying == false)
        {
            //AudioManagerController.instance.PlayTitleTheme();
            if(playLogoSFX) { AudioManagerController.instance.PlaySFX(20);  }
            

        }

        if(_continueBTN != null)
        {
            if (PlayerPrefs.HasKey("LastScene"))
            {
                _continueBTN.SetActive(true);
            }
        }
       
    }


    void Update() { }

    public void FadeIn()
    {
        _animatorFadeFX.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        _animatorFadeFX.SetTrigger("FadeOut");
    }

    // BTN ACTIONS

    public void NewGame()
    {
        //New game
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(startGameScene);
        //StartCoroutine("StartTheGame");
    }


    public void Tutorial()
    {
        //New game
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(tutorialScene);
    }


    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }


    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }


    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }


    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("QUIT GAME");
    }


    private IEnumerator StartTheGame()
    {
        //ship.SetTrigger("StartEngine");

        //yield return new WaitForSeconds(1f);

        //ship.SetTrigger("GoGame");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(startGameScene);

    }

}
