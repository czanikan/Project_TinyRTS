using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    List<RadialMenuEntry> Entries;

    Canvas parentCanvas;

    [SerializeField]
    int numberOfEntries;

    [SerializeField]
    GameObject ParentTile;

    [SerializeField]
    GameObject EntryPrefab;

    [SerializeField]
    List<Texture> icons;

    [SerializeField]
    List<string> labels;

    [SerializeField]
    List<GameObject> buildings;

    [SerializeField]
    GameObject targetObject;

    [SerializeField]
    float radius;

    bool isActive = false;

    private void Awake()
    {
        Entries = new List<RadialMenuEntry>();
        parentCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        parentCanvas.gameObject.SetActive(false);
    }

    void AddEntry(string pLabel, Texture pIcon, RadialMenuEntry.RadialMenuEntryDelegate pCallback)
    {
        GameObject entry = Instantiate(EntryPrefab, transform);

        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);
        rme.SetCallback(pCallback);

        Entries.Add(rme);
    }

    public void SetTile(GameObject tile)
    {
        targetObject = tile;
    }

    public int EntriesCount()
    {
        return Entries.Count;
    }

    public void Open()
    {
        //transform.position = Input.mousePosition;

        for (int i = 0; i < numberOfEntries; i++)
        {
            AddEntry(labels[i], icons[i], Instantiate);
        }
        Rearrange();
    }

    public void Close()
    {
        for (int i = 0; i < numberOfEntries; i++)
        {
            RectTransform rect = Entries[i].GetComponent<RectTransform>();
            GameObject entry = Entries[i].gameObject;

            rect.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuad);
            rect.DOAnchorPos(Vector3.zero, 0.3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Destroy(entry);
                };
        }

        Entries.Clear();
    }

    public void Toggle()
    {
        if(Entries.Count == 0)
        {
            isActive = true;
            parentCanvas.gameObject.SetActive(true);
            Open(); 
        }
        else
        {
            isActive = false;
            parentCanvas.gameObject.SetActive(false);
            Close();
        }
    }

    void Rearrange()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / Entries.Count;
        for (int i = 0; i < Entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i) * radius;
            float y = Mathf.Cos(radiansOfSeparation * i) * radius;

            RectTransform rect = Entries[i].GetComponent<RectTransform>();

            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), 0.3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
        }
    }

    void Instantiate(RadialMenuEntry pEntry) 
    {
        //targetIcon.texture = pEntry.GetIcon();
        //we need to build here

        GameObject ph = ParentTile.transform.Find("PlaceHolder").gameObject;
        GameObject b = Instantiate(buildings[findIndex(Entries, pEntry)], ph.transform);
        b.transform.localScale = Vector3.zero;
        b.transform.DOScale(Vector3.one * 1.5f, 0.2f).SetEase(Ease.OutQuad).onComplete = 
            delegate
            {
                b.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
            };
        b.transform.rotation = Quaternion.Euler(0, Random.Range(1, 6) * 60, 0);
        b.transform.parent = ph.transform;
        Close();
    }

    int findIndex(List<RadialMenuEntry> Entries, RadialMenuEntry pEntry)
    {
        for(int i = 0; i <= Entries.Count; i++)
        {
            if(Entries[i] == pEntry)
            {
                return i;
            }
        }
        return -1;
    }
}
