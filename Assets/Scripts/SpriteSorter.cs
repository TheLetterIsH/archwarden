using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteSorter : MonoBehaviour {

    [SerializeField] private bool isOneTime;
    [SerializeField] private float positionOffsetY;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int) (-(transform.position.y + positionOffsetY) * precisionMultiplier);

        if (isOneTime) {
            Destroy(this);
        }
    }
}
