using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private int wood = 0;
    [SerializeField] private int food = 0;
    [SerializeField] private int money = 0;
    [SerializeField] private int population = 0;

    [Header("UI Display References")]
    [SerializeField] private Text woodLabel;
    [SerializeField] private Text foodLabel;
    [SerializeField] private Text moneyLabel;
    [SerializeField] private Text populationLabel;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        woodLabel.text = wood.ToString();
        foodLabel.text = food.ToString();
        moneyLabel.text = money.ToString();
        populationLabel.text = population.ToString();
    }

    public void IncreaseWood(int amount)
    {
        wood += amount;
        UpdateUI();
    }

    public void IncreaseFood(int amount)
    {
        food += amount;
        UpdateUI();
    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void IncreasePopulation(int amount)
    {
        population += amount;
        UpdateUI();
    }
}
