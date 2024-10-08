using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Resource Type", order = 2)]
public class ResourceTypeSO : ScriptableObject {

    public string resourceName;
    public string resourceNameShort;
    public Sprite sprite;
    public string colorHex;

}
