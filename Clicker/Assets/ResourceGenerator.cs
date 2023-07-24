using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField]
    string resourceName;
    [SerializeField]
    Resource associatedResource;
    //number of generators
    int generatorUnits;
    //number of resources given per second
    float resourcePerSecond = 1;

    float baseCost = 10;
    float costMultiplier = 1.0f;

    float finalResourcePerSecond;
    Button buyButton;

    private void Start()
    {
        associatedResource = GameObject.Find(resourceName).GetComponent<Resource>();
    }

    public void AddGeneratorUnit()
    {
        if (associatedResource.storedResource >= baseCost * costMultiplier)
        {
            associatedResource.storedResource -= baseCost * costMultiplier;
            generatorUnits += 1;
            costMultiplier = 1.0f + (generatorUnits * 0.1f);
            CalculateResourcesPerSecond();
        }
    }

    public void CalculateResourcesPerSecond()
    {
        finalResourcePerSecond = generatorUnits * resourcePerSecond;
        associatedResource.resourcesPerSecond = finalResourcePerSecond;
    }
}
