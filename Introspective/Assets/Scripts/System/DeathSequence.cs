using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSequence : MonoBehaviour
{
    public Animator deathAnim;

    public void RunSequence()
    {
        deathAnim.SetBool("Dead", true);

        Invoke("End", 4f);
    }

    public void End()
    {
        SceneManager.LoadScene(1);
    }
}
