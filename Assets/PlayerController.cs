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
    public float playerJumpPower = 30f;
    public float maxVelocity = 3f;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    private float playerVelocity = 1;
    private float playerScore = 0;
    private bool isJump = false;
    private bool isDead = false;
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
        if (!isDead)
        {
            PlayerVelocity += 0.02f * Time.deltaTime;
            //Debug.Log(PlayerVelocity);
            if (PlayerVelocity > maxVelocity) PlayerVelocity = maxVelocity;
            playerScore += Time.deltaTime * 10f;
            PlaySceneManager.Instance.SetScore((int)playerScore);
            if (Input.GetKeyDown(KeyCode.Space))
                PlayerJump();
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetInt("HighScore", 0);
                PlaySceneManager.Instance.RestartGame();
            }
        }
    }

    void PlayerJump()
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
        isDead = true;
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
        Debug.Log(_col.gameObject.tag);
        if (_col.gameObject.CompareTag("AutoBox"))
        {
            PlayerJump();
        }
        else
        {
            if (!isDead)
            {
                PlaySceneManager.Instance.RestartGame();
                PlayerDead();
            }
        }

    }
}
