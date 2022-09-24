using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadScene : MonoBehaviour
{
    public string sceneToLoad;

    void Start()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
