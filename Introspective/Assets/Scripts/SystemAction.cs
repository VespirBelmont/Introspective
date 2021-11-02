using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemAction : MonoBehaviour
{
    public void ChangeScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }
}
