using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    Canvas canvas;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, 
            Camera.main.transform.rotation * Vector3.up);

        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        float UIScale = Mathf.Clamp(dist/1000, 0.005f, 0.1f);
        transform.localScale = new Vector3(UIScale, UIScale, UIScale);
    }
}
