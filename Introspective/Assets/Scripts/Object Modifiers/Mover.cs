using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 0.1f;


    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position -= new Vector3(moveSpeed, 0);
    }
}
