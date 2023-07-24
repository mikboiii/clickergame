using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Instantiate(minerPrefab, new Vector3(Random.Range(-9f, 0f), -0.5f, 0), Quaternion.identity);
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
