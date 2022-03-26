using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveVelocity = 10f;
    public Transform startTf;
    public Transform endTf;

    public Animator enemyAnim;

    void Start()
    {
        transform.position = startTf.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= moveVelocity * transform.forward * Time.deltaTime
            * PlayerController.Instance.PlayerVelocity;
        enemyAnim.SetFloat("enemySpeed", PlayerController.Instance.PlayerVelocity);
        if (transform.position.z < endTf.position.z)
            DestroyImmediate(gameObject);
    }
}
