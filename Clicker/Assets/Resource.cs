using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MonoBehaviour
{
    public TMP_Text resourceText;
    public string resourceName;

    [SerializeField]
    int baseClickIncrement;
    [SerializeField]
    int clickMultiplier;
    [SerializeField]
    public float storedResource;
    [SerializeField]
    public float resourcesPerSecond;
    [SerializeField]
    TextMeshProUGUI resourcePerSecondText;

    private void Start()
    {
        UpdateResourcePerSecond();

    }
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
    public void UpdateResourcePerSecond()
    {
        resourcePerSecondText.text = "Resource per second: " + resourcesPerSecond;
    }
}
