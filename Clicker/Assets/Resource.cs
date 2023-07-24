using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MonoBehaviour
{
    public TMP_Text resourceText;
    public string resourceName;

    public static Resource instance;

    [SerializeField]
    int baseClickIncrement;
    [SerializeField]
    int clickMultiplier;
    [SerializeField]
    public float storedResource;
    [SerializeField]
    public float resourcesPerSecond;
    public void ResourceClick()
    {
        storedResource += baseClickIncrement * clickMultiplier;
    }
    void textUpdate()
    {
        resourceText.text = resourceName + ": " + Mathf.Floor(storedResource);
    }
    private void Update()
    {
        storedResource += resourcesPerSecond * Time.deltaTime;
        textUpdate();
    }
}
