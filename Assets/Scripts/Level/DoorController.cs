using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float _distanceToOpen;
    [SerializeField] private bool _playerExiting;
    private PlayerController _player;
    private Animator _doorAnimator;
   

    // Door 
    public bool doorOpen;
    public Transform exitPoint;
    public float moveTowardSpeed;

    public string nextScene;

    void Start()
    {
        _player = PlayerHealthController.instance.gameObject.GetComponent<PlayerController>();
        _doorAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, _player.transform.position) <= _distanceToOpen)
        {
            doorOpen = true;

            //SFX
            AudioManagerController.instance.PlaySFX(22);

            Debug.Log("Suena Sonido");
        }

        else
        {
            doorOpen = false;

            //SFX
            AudioManagerController.instance.PlaySFX(22);
        }

        if (_playerExiting)
        {
            _player.transform.position = Vector2.MoveTowards(_player.transform.position, exitPoint.position, moveTowardSpeed * Time.deltaTime);
        }

        //Animation
        _doorAnimator.SetBool("isOpen", doorOpen);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (!_playerExiting)
            {
                _player.playerCanMove = false;

                StartCoroutine("UseDoor");
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
