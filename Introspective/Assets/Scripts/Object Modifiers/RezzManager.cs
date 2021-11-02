using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RezzManager : MonoBehaviour
{

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
