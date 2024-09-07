using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private ResourceGeneratorData resourceGeneratorData;
    private float time;
    private float maxTime;

    private void Awake() {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        maxTime = resourceGeneratorData.maxTime;
    }

    private void Start() {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in collider2DArray) {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode != null) {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType) {
                    nearbyResourceAmount++;
                }
            }
        }

        // For Base
        BuildingTypeSO buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        if (buildingType.buildingName == "Base") {
            nearbyResourceAmount = 1;
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

        if (nearbyResourceAmount == 0) {
            enabled = false;
        }
        else {
            maxTime = (
                (resourceGeneratorData.maxTime / 2f) +
                resourceGeneratorData.maxTime *
                (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount)
            );
        }

        Debug.Log("Nearby Resource Amount: " + nearbyResourceAmount + "; Max Time: " + maxTime);
    }

    private void Update() {
        time -= Time.deltaTime;

        if (time <= 0f) {
            time = maxTime;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

}
