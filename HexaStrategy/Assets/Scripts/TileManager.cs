using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private bool canBuild;

    void Start()
    {
        cam = Camera.main;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.GetComponent<Tile>() != null)
                {
                    // tengerszint felett van-e
                    if (hit.collider.gameObject.GetComponent<Tile>().transform.position.y >= 5)
                    {
                        // nem takarja UI
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            // üres-e
                            if (hit.collider.gameObject.GetComponent<Tile>().placeHolder.transform.childCount == 0)
                            {
                                Debug.Log("Buildable");
                                ClosePrevRadialMenus();
                                RadialMenu rm = hit.collider.transform.Find("Canvas/RadialMenu").GetComponent<RadialMenu>();
                                rm.Toggle();
                            }
                            else
                            {
                                Debug.Log(hit.collider.gameObject.GetComponent<Tile>().placeHolder.transform.GetChild(0).name);
                            }
                        }
                    }
                }
            }
        }
    }

    void ClosePrevRadialMenus()
    {
        RadialMenu[] rms = GameObject.FindObjectsOfType<RadialMenu>();

        foreach (RadialMenu rm in rms)
        {
            if (rm.EntriesCount() != 0)
                rm.Toggle();
        }
    }
}
