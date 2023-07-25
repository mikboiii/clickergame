using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceGenerator : MonoBehaviour
{
    //this is the name of the resource being produced
    //IMPORTANT: must be the name of the gameobject handling it
    [SerializeField]
    string resourceName;
    //this is the resource that the generator will produce
    [SerializeField]
    Resource associatedResource;
    //the prefab for miner objects
    [SerializeField]
    GameObject minerPrefab;
    //number of generators
    int generatorUnits;
    //number of resources given per second
    float resourcePerSecond = 1;
    //multiplier on the resources per second
    float upgradeMultiplier = 1;
    //number of upgrades bought
    float upgradeUnits;
    //upgrade cost
    float upgradeCost = 100;
    float cost;

    float finalResourcePerSecond;
    [SerializeField]
    TextMeshProUGUI costText;
    [SerializeField]
    TextMeshProUGUI upgradeCostText;
    Button buyButton;

    private void Start()
    {
        //cost formula = 1/2x^2 + 10
        cost = Mathf.Floor(10 + (0.5f * Mathf.Pow(generatorUnits, 2f)));
        upgradeCost = Mathf.Floor(100 + (25f * Mathf.Pow(upgradeUnits, 2f)));
        associatedResource = GameObject.Find(resourceName).GetComponent<Resource>();
        UpdateCost();
    }

    public void AddGeneratorUnit()
    {
        if (associatedResource.storedResource >= cost)
        {
            Instantiate(minerPrefab, new Vector3(Random.Range(-9f, 0f), -0.5f, 0), Quaternion.identity);
            associatedResource.storedResource -= cost;
            generatorUnits += 1;
            CalculateResourcesPerSecond();
            associatedResource.UpdateResourcePerSecond();
            UpdateCost();
        }
    }

    public void CalculateResourcesPerSecond()
    {
        finalResourcePerSecond = generatorUnits * resourcePerSecond * upgradeMultiplier;
        associatedResource.resourcesPerSecond = finalResourcePerSecond;
        associatedResource.UpdateResourcePerSecond();
    }

    public void BuyUpgrade()
    {
        //need steeper curve as the benefit is bigger
        //cost = 100 + 25x^2
        if (associatedResource.storedResource >= upgradeCost)
        {
            upgradeMultiplier *= 2;
            associatedResource.storedResource -= upgradeCost;
            upgradeUnits += 1;
            UpdateCost();
            CalculateResourcesPerSecond();
        }
    }

    public void UpdateCost()
    {
        cost = Mathf.Floor(10 + (0.9f * Mathf.Pow(generatorUnits, 2f)));
        upgradeCost = Mathf.Floor(100 + (250f * Mathf.Pow(upgradeUnits, 2f)));
        costText.text = "Cost: "+cost.ToString();
        upgradeCostText.text = "Cost: " + upgradeCost.ToString();
    }
}
