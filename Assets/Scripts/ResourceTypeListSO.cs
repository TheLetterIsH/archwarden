using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Resource Type List", order = 3)]
public class ResourceTypeListSO : ScriptableObject {
    
    public List<ResourceTypeSO> list;

}
