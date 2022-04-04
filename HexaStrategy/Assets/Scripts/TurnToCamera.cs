using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);

        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        float UIScale = Mathf.Clamp(dist / 1000, 0.5f, 2f);
        transform.localScale = new Vector3(UIScale, UIScale, UIScale);
    }
}
