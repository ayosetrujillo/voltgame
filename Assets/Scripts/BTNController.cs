using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTNController : MonoBehaviour
{
    public string nextSceneToLoad;
    public string titleScreenScene;
    public string tutorialScene;
    public string newGameScene;
    public string optionsScene;
    public string creditsScene;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(titleScreenScene);
    }

    public void TutorialScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(tutorialScene);
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(newGameScene);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    public void Options()
    {
        SceneManager.LoadScene(optionsScene);
    }

    public void Credits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT GAME");
    }






}
