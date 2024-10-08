using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake() {
        Instance = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }

    private void LogResourceAmountDictionary() {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
            Debug.Log(resourceType.resourceName + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public bool CanAffordResources(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amoumt) {
                
            } else {
                return false;
            }
        }

        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amoumt;
        }
    }

}
