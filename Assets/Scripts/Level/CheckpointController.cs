using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    private Animator _animator;
    private string _currentScene;
    private GameObject _player;
    public bool isActive;
    public PlayerAbilityManager playerAbility;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentScene = SceneManager.GetActiveScene().name.ToString();
        isActive = false;

        _player = GameObject.Find("Player");
        playerAbility = _player.GetComponent<PlayerAbilityManager>();

        if (PlayerPrefs.HasKey("LastScene"))
        {
           StartCoroutine("LoadProgress", _player.GetComponentInChildren<Collider2D>());
        }
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerHealthController>().TotalCureHP();

            //RespawnController.instance.SetNewRespawm(collision.transform.position);
            RespawnController.instance.SetNewCheckpoint(collision.transform.position);
            

            _animator.SetBool("Check", true);

            if (!isActive) {
                //SFX
                AudioManagerController.instance.PlaySFX(16);
                PlayerHealthController.instance.TotalCureHP();
                isActive = true;
            }



            // Saving current progress //
            StartCoroutine("SaveProgress", _player.GetComponentInChildren<Collider2D>());
            
        }
    }

    public IEnumerator SaveProgress(Collider2D _collision)
    {
        Debug.Log("Name Scene: " + _currentScene);

        //Player Position
        PlayerPrefs.SetFloat("PosX", _collision.transform.position.x);
        PlayerPrefs.SetFloat("PosY", _collision.transform.position.y);
        PlayerPrefs.SetFloat("PosZ", _collision.transform.position.z);
        PlayerPrefs.SetString("LastScene", _currentScene);

        //Player Abilities
        
        if (playerAbility.doubleJump)   { PlayerPrefs.SetInt("doubleJump",  1); }    else    { PlayerPrefs.SetInt("doubleJump",  0); }
        if (playerAbility.dash)         { PlayerPrefs.SetInt("dash",        1); }    else    { PlayerPrefs.SetInt("dash",        0); }
        if (playerAbility.morphBall)    { PlayerPrefs.SetInt("morphBall",   1); }    else    { PlayerPrefs.SetInt("morphBall",   0); }
        if (playerAbility.dropBombs)    { PlayerPrefs.SetInt("dropBombs",   1); }    else    { PlayerPrefs.SetInt("dropBombs",   0); }
        if (playerAbility.surf)         { PlayerPrefs.SetInt("surf",        1); }    else    { PlayerPrefs.SetInt("surf",        0); }


        /*Player Keys

        if (UIController.instance.hasKey1) { PlayerPrefs.SetInt("key1", 1); } else { PlayerPrefs.SetInt("key1", 0); }
        if (UIController.instance.hasKey2) { PlayerPrefs.SetInt("key2", 1); } else { PlayerPrefs.SetInt("key2", 0); }
        if (UIController.instance.hasKey3) { PlayerPrefs.SetInt("key3", 1); } else { PlayerPrefs.SetInt("key3", 0); }

        if (UIController.instance.hasBossKey) { PlayerPrefs.SetInt("HasBossKey", 1); } else { PlayerPrefs.SetInt("HasBossKey", 0); } */


        yield return new WaitForSeconds(0.2f);

        Debug.Log("SAVE Completed");
    }

    public IEnumerator LoadProgress(Collider2D _collision)
    {
        Debug.Log("Name Scene: " + _currentScene);

       /* // Player Position
        PlayerPrefs.GetFloat("PosX", _collision.transform.position.x);
        PlayerPrefs.GetFloat("PosY", _collision.transform.position.y); 
        PlayerPrefs.GetFloat("PosZ", _collision.transform.position.z); */


        // Player Ability
        if (PlayerPrefs.GetInt("doubleJump")    == 1)   { playerAbility.doubleJump  = true; } else { playerAbility.doubleJump   = false; }
        if (PlayerPrefs.GetInt("dash")          == 1)   { playerAbility.dash        = true; } else { playerAbility.dash         = false; }
        if (PlayerPrefs.GetInt("morphBall")     == 1)   { playerAbility.morphBall   = true; } else { playerAbility.morphBall    = false; }
        if (PlayerPrefs.GetInt("dropBombs")     == 1)   { playerAbility.dropBombs   = true; } else { playerAbility.dropBombs    = false; }
        if (PlayerPrefs.GetInt("surf")          == 1)   { playerAbility.surf        = true; } else { playerAbility.surf         = false; }

        /* Boss Key
        if(PlayerPrefs.HasKey("HasBossKey"))
        {
            UIController.instance.bossKey.SetActive(true);
            UIController.instance.hasBossKey = true;
        }

        // Key 1
        if (PlayerPrefs.HasKey("key1"))
        {
            UIController.instance.key1.SetActive(true);
            UIController.instance.hasKey1 = true;
        }

        // Key 2
        if (PlayerPrefs.HasKey("key2"))
        {
            UIController.instance.key2.SetActive(true);
            UIController.instance.hasKey2 = true;
        }

        // Key 3
        if (PlayerPrefs.HasKey("key3"))
        {
            UIController.instance.key3.SetActive(true);
            UIController.instance.hasKey3 = true;
        } */


        yield return new WaitForSeconds(0.2f);
        Debug.Log("LOAD Completed");
    }

}
