using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    private static Camera mainCamera;

    private static Vector3 GetMouseWorldPosition() {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }

    public static Vector3 GetSnappedMouseWorldPosition() {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        mouseWorldPosition.x = Mathf.RoundToInt(mouseWorldPosition.x);
        mouseWorldPosition.y = Mathf.RoundToInt(mouseWorldPosition.y);

        return mouseWorldPosition;
    }

}
