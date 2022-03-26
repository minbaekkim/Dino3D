using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public float PlayerVelocity
    {
        get
        {
            return playerVelocity;
        }

        set
        {
            playerVelocity = value;
            playerAnim.SetFloat("playerSpeed", value);
        }
    }
    public bool isAutoMode;
    public float maxVelocity;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    private float playerJumpPower = 50f;
    private float playerVelocity = 1;
    private float playerScore = 0;
    private bool isJump = false;

    public bool IsDead{ get; set;}

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (!IsDead)
        {
            PlayerVelocity += 0.02f * Time.deltaTime;
            if (PlayerVelocity > maxVelocity) PlayerVelocity = maxVelocity;

            playerScore += Time.deltaTime * 20f;
            if(playerScore > 99999) playerScore = 99999;
            PlaySceneManager.Instance.SetScore((int)playerScore);
            
            if (Input.GetKeyDown(KeyCode.Space))
                PlayerJump();
        }
    }

    public void PlayerJump()
    {
        if (!isJump)
        {
            isJump = true;
            playerAudio.Play();
            playerAnim.SetBool("isJump", true);
            playerRb.AddForce(Vector3.up * playerJumpPower, ForceMode.Impulse);
        }
    }

    void PlayerDead()
    {
        IsDead = true;
        PlayerVelocity = 0f;
        playerAnim.SetTrigger("isDead");
        PlaySceneManager.Instance.EndGame();
    }

    private void OnCollisionEnter(Collision _col)
    {
        if(isJump)
        {
            isJump = false;
            playerAnim.SetBool("isJump", false);
        }
    }

    private void OnTriggerEnter(Collider _col)
    {

        if(isAutoMode){
            if (_col.gameObject.CompareTag("AutoBox")) PlayerJump();
            else{
                PlayerDead();
                PlaySceneManager.Instance.RestartGame();
            }
            return;
        }

        if (!IsDead)
            if (!_col.gameObject.CompareTag("AutoBox")) PlayerDead();
    }
}