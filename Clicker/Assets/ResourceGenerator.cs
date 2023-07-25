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

    float cost;

    float finalResourcePerSecond;
    [SerializeField]
    TextMeshProUGUI costText;
    Button buyButton;

    private void Start()
    {
        //cost formula = 1/2x^2 + 10
        cost = Mathf.Floor(10 + (0.5f * Mathf.Pow(generatorUnits, 2f)));
        associatedResource = GameObject.Find(resourceName).GetComponent<Resource>();
    }

    public void AddGeneratorUnit()
    {
        if (associatedResource.storedResource >= cost)
        {
            Instantiate(minerPrefab, new Vector3(Random.Range(-9f, 0f), -0.5f, 0), Quaternion.identity);
            associatedResource.storedResource -= cost;
            generatorUnits += 1;
            CalculateResourcesPerSecond();
            cost = Mathf.Floor(10 + (0.5f * Mathf.Pow(generatorUnits, 2f)));
        }
        costText.text = cost.ToString();
    }

    public void CalculateResourcesPerSecond()
    {
        finalResourcePerSecond = generatorUnits * resourcePerSecond;
        associatedResource.resourcesPerSecond = finalResourcePerSecond;
    }
}
