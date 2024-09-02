using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {
    private BuildingTypeSO buildingType;
    private float time;
    private float maxTime;

    private void Awake() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        maxTime = buildingType.resourceGeneratorData.maxTime;
    }

    private void Update() {
        time -= Time.deltaTime;

        if (time <= 0f) {
            time = maxTime;
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 1);
        }
    }
}
