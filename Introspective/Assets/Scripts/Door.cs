using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Close()
    {
        this.GetComponent<Animator>().SetBool("Closed", true);
    }
}
