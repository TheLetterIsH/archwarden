using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position) {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in collider2DArray) {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode != null) {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType) {
                    nearbyResourceAmount++;
                }
            }
        }

        // For Base - to be refactored
        if (resourceGeneratorData.resourceType.resourceName == "Mana") {
            nearbyResourceAmount = 1;
        }

        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

        return nearbyResourceAmount;
    }

    private ResourceGeneratorData resourceGeneratorData;
    private float time;
    private float maxTime;

    private void Awake() {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        maxTime = resourceGeneratorData.maxTime;
    }

    private void Start() {
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);


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

    public ResourceGeneratorData GetResourceGeneratorData() {
        return resourceGeneratorData;
    }

    public float GetTimeNormalized() {
        return time / maxTime;
    }

    public float GetAmountGeneratedPerSecond() {
        return 1 / maxTime;
    }

}
