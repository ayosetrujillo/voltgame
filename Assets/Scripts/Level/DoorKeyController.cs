using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorKeyController : MonoBehaviour
{

    private float _distanceToOpen = 6f;
    private bool _playerExiting;
    private PlayerController _player;
    private Animator _doorAnimator;

    public GameObject keyStopper;
    public string keyName;

    // Door 
    public bool doorOpen;

    [HideInInspector] public Transform exitPoint;
    [HideInInspector] public float moveTowardSpeed;
    

    public string nextScene;

    void Start()
    {
        _player = PlayerHealthController.instance.gameObject.GetComponent<PlayerController>();
        _doorAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // KEY REQUIRED
        if (PlayerPrefs.HasKey(keyName))
        {
            keyStopper.SetActive(false);

            // DON'T NEED KEY, THE DOOR IS OPEN AUTOMATICALLY
            if (Vector3.Distance(transform.position, _player.transform.position) <= _distanceToOpen)
            {
                doorOpen = true;
            }
            else
            {
                doorOpen = false;
            }

            if (_playerExiting)
            {
                _player.transform.position = Vector2.MoveTowards(_player.transform.position, exitPoint.position, moveTowardSpeed * Time.deltaTime);
            }

            //Animation
            _doorAnimator.SetBool("isOpen", doorOpen);

        }
        else
        { // THE PLAYER DON'T HAVE THE KEY

            doorOpen = false;
            keyStopper.SetActive(true);

            //Animation
            _doorAnimator.SetBool("isOpen", doorOpen);

            //MSG
            Debug.Log("NO TIENES LA LLAVE");


        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UIController.instance.hasBossKey || UIController.instance.hasKey1 || UIController.instance.hasKey2 || UIController.instance.hasKey3)
        {
            keyStopper.SetActive(false);
                
            if (collision.CompareTag("Player"))
            {
                if (!_playerExiting)
                {
                    _player.playerCanMove = false;
                    StartCoroutine("UseDoor");
                }
            }
        }

    }


    IEnumerator UseDoor()
    {
        _playerExiting = true;
        UIController.instance.FadeIn();

        yield return new WaitForSeconds(1f);

        UIController.instance.FadeOut();
        RespawnController.instance.SetNewRespawm(exitPoint.position);
        _playerExiting = false;
        _player.playerCanMove = true;

        SceneManager.LoadScene(nextScene);
    }
}
