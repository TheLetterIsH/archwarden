using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour {

    public ResourceTypeSO resourceType;

    private GameObject sprite;

    private void Start() {
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;

        int rotationZ = Random.Range(0, 3) * 90;
        sprite.transform.Rotate(0, 0, rotationZ);
    }

}
