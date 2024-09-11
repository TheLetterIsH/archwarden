using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type", order = 0)]
public class BuildingTypeSO : ScriptableObject {

    public string buildingName;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;

    public string GetConstructionResourceCostString() {
        string result = "";

        foreach (ResourceAmount resourceAmount in constructionResourceCostArray) {
            result += 
                "<color=#" + resourceAmount.resourceType.colorHex + ">" + 
                resourceAmount.resourceType.resourceNameShort + 
                resourceAmount.amoumt + "</color> ";
        }

        return result;
    }
}
