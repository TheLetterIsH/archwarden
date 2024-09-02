using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Building Type List", order = 1)]
public class BuildingTypeListSO : ScriptableObject {

    public List<BuildingTypeSO> list;

}
