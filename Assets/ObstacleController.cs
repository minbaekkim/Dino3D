using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float moveVelocity = 1f;
    public Transform startTf;
    public Transform endTf;
    void Start()
    {
        transform.position = startTf.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= moveVelocity * transform.forward * Time.deltaTime
            *PlayerController.Instance.PlayerVelocity;
        if (transform.position.z < endTf.position.z)
            DestroyImmediate(gameObject);
    }   
}
