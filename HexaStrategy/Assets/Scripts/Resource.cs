using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool isAvailable;

    private void Start()
    {
        isAvailable = true;
    }

    public void ChangeState()
    {
        isAvailable = !isAvailable;
    }
}
