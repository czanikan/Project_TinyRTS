using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GatherSystem : MonoBehaviour
{
    enum ResourceType
    {
        Wood,
        Food,
        Money,
        Population
    }

    [SerializeField] private ResourceType resourceType;
    [SerializeField] private float gatherTime;
    [SerializeField] private float gatherRange;

    private int gatherAmount;

    private Materials materials;
    private string nodeTag;

    [Header("Gather Popup Effect References")]
    [SerializeField] GameObject woodPopup;
    [SerializeField] GameObject foodPopup;
    [SerializeField] GameObject moneyPopup;
    [SerializeField] GameObject populationPopup;
    [SerializeField] private Transform popupSpawnPosition;

    void Start()
    {
        materials = GameObject.FindGameObjectWithTag("GameController").GetComponent<Materials>();
        gatherAmount = 0;

        switch (resourceType)
        {
            case ResourceType.Wood:
                nodeTag = "Forest";
                InvokeRepeating("SetGatherAmount", 1, 1);
                InvokeRepeating("GatherWood", gatherTime, gatherTime);
                break;
            case ResourceType.Food:
                nodeTag = "Field";
                InvokeRepeating("SetGatherAmount", 1, 1);
                InvokeRepeating("GatherFood", gatherTime, gatherTime);
                break;
            case ResourceType.Money:
                nodeTag = "House";
                InvokeRepeating("SetGatherAmount", 1, 1);
                InvokeRepeating("GatherMoney", gatherTime, gatherTime);
                break;
            case ResourceType.Population:
                break;
            default:
                Debug.LogError("Not a valid type for the enum!");
                break;
        }
    }

    private void GatherWood()
    {
        if (gatherAmount != 0)
        {
            materials.IncreaseWood(gatherAmount);
            transform.DOScale(Vector3.one * 1.5f, 0.25f).SetEase(Ease.OutQuad);
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutQuad);

            GameObject popupGO = Instantiate(woodPopup, popupSpawnPosition.position, popupSpawnPosition.rotation);
            popupGO.transform.DOMove(transform.position +
                new Vector3(Random.Range(-0.5f, 0.5f), 3, Random.Range(-0.5f, 0.5f)), 0.8f).SetEase(Ease.OutQuad);
            popupGO.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutQuad);
            popupGO.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad);
            Destroy(popupGO, 1f);
        }
    }

    private void GatherFood()
    {
        if (gatherAmount != 0)
        {
            materials.IncreaseFood(gatherAmount);
            transform.DOScale(Vector3.one * 1.5f, 0.25f).SetEase(Ease.OutQuad);
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutQuad);

            GameObject popupGO = Instantiate(foodPopup, popupSpawnPosition.position, popupSpawnPosition.rotation);
            popupGO.transform.DOMove(transform.position +
                new Vector3(Random.Range(-0.5f, 0.5f), 3, Random.Range(-0.5f, 0.5f)), 0.8f).SetEase(Ease.OutQuad);
            popupGO.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutQuad);
            popupGO.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad);
            Destroy(popupGO, 1f);
        }
    }

    private void GatherMoney()
    {
        if (gatherAmount != 0)
        {
            materials.IncreaseMoney(gatherAmount);
            transform.DOScale(Vector3.one * 1.5f, 0.25f).SetEase(Ease.OutQuad);
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutQuad);

            GameObject popupGO = Instantiate(moneyPopup, popupSpawnPosition.position, popupSpawnPosition.rotation);
            popupGO.transform.DOMove(transform.position +
                new Vector3(Random.Range(-0.5f, 0.5f), 3, Random.Range(-0.5f, 0.5f)), 0.8f).SetEase(Ease.OutQuad);
            popupGO.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.OutQuad);
            popupGO.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad);
            Destroy(popupGO, 1f);
        }
    }

    private void SetGatherAmount()
    {
        Collider[] nodesInRange = Physics.OverlapSphere(transform.position, gatherRange);

        int amount = 0;

        foreach(Collider collider in nodesInRange)
        {
            if (collider.gameObject.tag == nodeTag)
            {
                if (collider.gameObject.GetComponent<Resource>().isAvailable)
                {
                    collider.gameObject.GetComponent<Resource>().ChangeState();
                    amount++;
                } 
                
            }
        }
        
        gatherAmount += amount;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gatherRange);
    }


}
