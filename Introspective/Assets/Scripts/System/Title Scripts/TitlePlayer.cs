using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public void Run()
    {
        this.GetComponent<Animator>().SetBool("GameStarted", true);
    }
}
