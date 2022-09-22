using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // Main ref
    private Rigidbody2D _playerRigid2D;
    private Animator _playerAnimator;
    private Animator _playerBallAnimator;
    private PlayerAbilityManager _playerAbility;

    [Space(10)]
    [Header("// PLAYER MOVEMENT //")]

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _playerJump;
    [SerializeField] private float _coyoteTime = 0.16f;
    [SerializeField] private Transform _playerGroundCheck;
    [SerializeField] private LayerMask _groundLayer;

    [Space(10)]
    [Header("// PLAYER ENERGY //")]

    [SerializeField] private int _bonusLimit = 85;
    [SerializeField] private GameObject _lowBatteryNotification;

    private float _timeInAir; //coyote time counter

    [Space(10)]
    [Header("// MAIN WEAPON //")]

    // Weapons var
    public BulletController bulletPrefab;
    public BulletController bulletBigPrefab;
    public GameObject failBulletPrefab;
    public Transform shootPoint;

    [Space(10)]
    [Header("// MELEE ATTACK //")]

    //Melee Attack
    private Animator _hitboxAnimator;
    public GameObject hitboxGameObject;


    [Space(10)]
    [Header("// DASH //")]

    // Dash var
    public float dashTime;
    public float dashSpeed = 20;
    private float _dashCounter;

    // Dash Cooldown var
    public float dashCoolDown = 1f;
    private float _timerCoolDown;

    // Dash effect
    public SpriteRenderer playerSprite;
    public SpriteRenderer afterImage;
    public Color afterImageColor;
    public float afterImageLifetime;
    public float timeBetweenCopies;

    private float _afterImageCounter;


    [Space(10)]
    [Header("// BALL MODE //")]

    // Ball Mode
    public GameObject playerStandObject;
    public GameObject playerBallObject;
    public BombController bombPrefab;
    [SerializeField] private GameObject _playerTubeCheck;

    [Space(5)]

    //Ball Weapon
    [Space(10)]
    [Header("// PLAYER STATES //")]

    [Space(10)]
    // Player STATES var
    public bool playerCanMove;
    public bool playerIsGrounded;
    public bool playerIsMoving;
    public bool playerIsJumping;
    public bool playerIsShooting;
    public bool playerIsDashing;
    public bool playerIsBall;
    public bool playerIsAttacking;
    public bool playerCanDoubleJump;
    public bool playerInTube;
    public bool playerInTrance;
    public bool playerIsLowBattery;
    public bool lookR = true;

    private void Awake()
    {
        _playerRigid2D = GetComponent<Rigidbody2D>();
        _playerAnimator = playerStandObject.GetComponentInChildren<Animator>();
        _hitboxAnimator = hitboxGameObject.GetComponentInChildren<Animator>();
        _playerBallAnimator = playerBallObject.GetComponent<Animator>();
        _playerAbility = GetComponent<PlayerAbilityManager>();

        instance = this;
    }

    void Start() { lookR = true; playerIsShooting = false; playerCanMove = true; }

    void Update()
    {

        // PLAYER STATES:


        if (PlayerEnergyController.instance.currentEnergy >= _bonusLimit) {

            playerInTrance = true;
            playerSprite.color = new Color(0.15f, 1f, 0.85f, 1f);  // Verde VOLT
            UIController.instance.iconEnergy.color = new Color(0.1f, 1f, 0.8f, 1f);  // Verde VOLT
        }

        else if (PlayerHealthController.instance.inmunity == true) {
            playerInTrance = false;
            playerSprite.color = Color.red;
            UIController.instance.iconEnergy.color = Color.white;
        } else
        {
            playerInTrance = false;
            playerSprite.color = Color.white;
            UIController.instance.iconEnergy.color = Color.white;
        }


        if (PlayerEnergyController.instance.currentEnergy <= 10) // Min Energy
        {
            playerIsLowBattery = true;
            _lowBatteryNotification.SetActive(true);
        }
        else
        {
            playerIsLowBattery = false;
            _lowBatteryNotification.SetActive(false);
        }



        // CAN'T MOVE PLAYER
        if (!playerCanMove) {
            //playerIsMoving = false;
            _playerRigid2D.velocity = Vector2.zero;
        }


        // CAN MOVE
        if (playerCanMove) {

            // Player Dash
            if (_timerCoolDown == 0) {
                if (Input.GetButtonDown("Dash") && !playerIsBall && _playerAbility.dash) { _dashCounter = dashTime; DashEffect(); }
            }

            // Player Energy Cure
            if (Input.GetButtonDown("Heal"))
            {
                if(PlayerEnergyController.instance.currentEnergy >= 85)
                {
                    PlayerHealthController.instance.HealPlayer(5);
                    PlayerEnergyController.instance.SpendEnergy(100);

                }
            }


            if ((_dashCounter > 0 ))
            {
                playerIsDashing = true;
                _dashCounter = _dashCounter - Time.deltaTime;
                _playerRigid2D.velocity = new Vector2(dashSpeed * transform.localScale.x, _playerRigid2D.velocity.y);
                _timerCoolDown = dashCoolDown;

                //Dash Effect
                _afterImageCounter -= Time.deltaTime;
                if(_afterImageCounter <= 0)
                {
                    DashEffect();
                }

            } else {
                playerIsDashing = false;
                _dashCounter = 0;
            }

            // Cooldowm Timer
            if(_timerCoolDown > 0) {
                _timerCoolDown = _timerCoolDown - Time.deltaTime;
            } else {
                _timerCoolDown = 0;
            }

            // Player horizontal movement
            if (!playerIsDashing && !playerIsAttacking)
            {

                //Player In Trance
                if(!playerInTrance)
                {
                    _playerRigid2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _playerSpeed, _playerRigid2D.velocity.y);
                } else
                {
                    _playerRigid2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _playerSpeed * 1.5f, _playerRigid2D.velocity.y);
                }
                


                // Is Moving with flip
                if (_playerRigid2D.velocity.x < 0) { playerIsMoving = true; transform.localScale = new Vector3(-1f, 1f, 1f); lookR = false; }
                if (_playerRigid2D.velocity.x > 0) { playerIsMoving = true; transform.localScale = new Vector3(1f, 1f, 1f);  lookR = true; }

                if (_playerRigid2D.velocity.x == 0) { playerIsMoving = false; }

                if(lookR == false)
                {
                    _lowBatteryNotification.transform.localScale = new Vector3(-1f, 1f, 1f);
                } else
                {
                    _lowBatteryNotification.transform.localScale = new Vector3(1f, 1f, 1f);
                }

            }

            // Is Grounded
            playerIsGrounded = Physics2D.OverlapCircle(_playerGroundCheck.position, 0.2f, _groundLayer);
            playerIsJumping = !playerIsGrounded;

            if (playerIsGrounded)
            {
                _timeInAir = 0;
            } else {
                _timeInAir += Time.deltaTime;
            }


            // Is Jumping
            if (Input.GetButtonDown("Jump") && (playerIsGrounded || playerCanDoubleJump || _timeInAir < _coyoteTime))
            {

                if (PlayerEnergyController.instance.currentEnergy >= 0)
                {
                    //SFX
                    AudioManagerController.instance.PlaySFX(6);

                    if (_playerAbility.doubleJump && PlayerEnergyController.instance.lowEnergy == false) // double jump ability check
                    {
                        if (playerIsGrounded)
                        {
                            playerCanDoubleJump = true;
                        }
                        else
                        {
                            playerCanDoubleJump = false;
                            // DOUBLE JUMP ANIMATION
                            _playerAnimator.SetTrigger("isDoubleJump");
                            //SFX
                            AudioManagerController.instance.PlaySFX(3);

                            //ENERGY
                            PlayerEnergyController.instance.SpendEnergy(20);
                        }
                    }
                    _playerRigid2D.velocity = new Vector2(_playerRigid2D.velocity.x, _playerJump);
                    playerIsJumping = true;
                } else
                {
                    Debug.Log("Sin Energía para doble salto");
                }

            }

            // Is Shooting
            if (Input.GetButtonDown("Fire1") && (!playerIsBall))
            {

                if (PlayerEnergyController.instance.lowEnergy == false)
                {
                    
                    if (!playerInTrance)
                    {
                        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation).bulletDir = new Vector2(transform.localScale.x, 0f);

                        //SFX
                        AudioManagerController.instance.PlaySFX(8);
                    }
                    else
                    {
                        //BIG BULLET
                        Instantiate(bulletBigPrefab, shootPoint.position, shootPoint.rotation).bulletDir = new Vector2(transform.localScale.x, 0f);

                        //SFX
                        AudioManagerController.instance.PlaySFX(18);
                    }
                } else
                {
                    Instantiate(failBulletPrefab, shootPoint.position, Quaternion.identity);
                    //SFX
                    AudioManagerController.instance.PlaySFX(17);
                }
           

                playerIsShooting = true;

                PlayerEnergyController.instance.SpendEnergy(10);

                // SHOOT ANIMATION
                _playerAnimator.SetTrigger("isShoot");

               
                playerIsAttacking = false;

            }

            if(Input.GetButtonUp("Fire1"))
            {
                playerIsShooting = false;
            }

            if (Input.GetButtonUp("Fire2"))
            {
                playerIsAttacking = false;
            }


            /* MELEE ATTACK WITH BLADE

            if (Input.GetButton("Fire2") && !playerIsAttacking)
            {
    

                if (playerIsBall == false && playerIsGrounded == true && playerIsJumping == false)
                {
                    _hitboxAnimator.SetTrigger("Hitbox");
                    _playerAnimator.SetTrigger("isAttacking");
                    _playerRigid2D.velocity = Vector2.zero;

                    //STATES
                    playerIsAttacking = true;
                    playerIsMoving = false;
                    playerIsShooting = false;
                    playerIsJumping = false;
                    playerIsBall = false;
                }
            } */



            // Ball Mode

            if (_playerAbility.morphBall && (playerBallObject != null) && (playerBallObject != null))
            {
                //ON
                if (!playerBallObject.activeSelf)
                {

                    if(Input.GetAxisRaw("Vertical") < -0.9)
                    {
                        playerStandObject.SetActive(false);
                        playerBallObject.SetActive(true);
                        playerIsBall = true;

                        playerIsAttacking   = false;

                        _playerTubeCheck.SetActive(false);

                        //SFX
                        AudioManagerController.instance.PlaySFX(0);

                    } 
                } else { //OFF

                    //Comprobamos si hay techo.
                    _playerTubeCheck.SetActive(true);
                    //playerInTube = Physics2D.OverlapCircle(_playerTubeCheck.transform.position, -0.2f, _groundLayer);
                    playerInTube = Physics2D.Raycast(_playerTubeCheck.transform.position, transform.TransformDirection(Vector3.up), 1f, _groundLayer);
                    Debug.DrawRay(_playerTubeCheck.transform.position, transform.TransformDirection(Vector3.up) * 1f, Color.yellow);

                    if (!playerInTube)
                    {

                        if (Input.GetAxisRaw("Vertical") > 0.9)
                        {
                            playerStandObject.SetActive(true);
                            playerBallObject.SetActive(false);
                            playerIsBall = false;

                            //SFX
                            AudioManagerController.instance.PlaySFX(15);


                        }

                    }
                    

                }

                // Is Shooting BOMB!
                if (Input.GetButtonDown("Fire1") && playerIsBall && _playerAbility.morphBall && _playerAbility.dropBombs)
                {
                    Instantiate(bombPrefab, playerBallObject.transform.position, playerBallObject.transform.rotation);
                    playerIsShooting = true;

                }

                if (Input.GetButtonUp("Fire1"))
                {
                    playerIsShooting = false;
                }
            }
        }

        // BASIC PLAYER ANIMATIONS /////

        if (!playerIsBall) {      
            _playerAnimator.SetBool("isGrounded",   playerIsGrounded);
            _playerAnimator.SetBool("isMoving",     playerIsMoving);
        }


        if (playerIsBall)
        {
            _playerBallAnimator.SetBool("isBallMoving", playerIsMoving);
        }

    }

    public void DashEffect()
    {

        AudioManagerController.instance.PlaySFX(1);

        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = playerSprite.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

        _afterImageCounter = timeBetweenCopies;
    }
    
}