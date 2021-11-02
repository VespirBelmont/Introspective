using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public void Deactivates()
    {
        this.gameObject.SetActive(false);
    }
}
