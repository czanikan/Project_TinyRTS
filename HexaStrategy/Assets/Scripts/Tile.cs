using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    public GameObject placeHolder;

    [SerializeField] private int detailChance = 70;
    public GameObject details;

    private Vector3 scaleChange;

    void Start()
    {
        scaleChange = new Vector3(0.2f, 0.2f, 0.2f);

        if(transform.position.y >= 5)
        {
            if (Random.Range(1, 100) < detailChance)
            {
                GameObject TempGO = Instantiate(details);
                TempGO.transform.parent = placeHolder.transform;
                TempGO.transform.rotation = Quaternion.Euler(0, Random.Range(1, 6) * 60, 0);
                TempGO.transform.position = placeHolder.transform.position;
            }
        }
        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (transform.position.y >= 5)
        {
            transform.DOScale(Vector3.one * 1.2f, 0.3f).SetEase(Ease.OutQuad);
        }
    }

    private void OnMouseExit()
    {
        if (transform.position.y >= 5)
        {
            transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
        }
    }
}
