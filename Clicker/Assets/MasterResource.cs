using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterResource : MonoBehaviour
{
    public List<Resource> resourceList;

    private void Start()
    {
        resourceList = InitResources();
    }

    private List<Resource> InitResources()
    {
        List<Resource> resourceReturn = new List<Resource>();
        foreach(Resource thing in FindObjectsOfType<Resource>())
        {
            resourceReturn.Add(thing);
        }
        return resourceReturn;
    }
}