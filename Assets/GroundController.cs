using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float moveVelocity = 1f;

    void Update()
    {
        transform.position -= moveVelocity * transform.forward * Time.deltaTime
            * PlayerController.Instance.PlayerVelocity;
        if (transform.position.z < -2400)
            transform.position += Vector3.forward * 2400f;
    }
}
