using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance {  get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs {
        public BuildingTypeSO activeBuildingType;
    }

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private void Awake() {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (activeBuildingType != null) {
                if (CanSpawnBuilding(activeBuildingType, Utils.GetSnappedMouseWorldPosition(), out string errorMesssage)) {
                    if (ResourceManager.Instance.CanAffordResources(activeBuildingType.constructionResourceCostArray)) {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                    
                        Instantiate(activeBuildingType.prefab, Utils.GetSnappedMouseWorldPosition(), Quaternion.identity);
                    }
                    else {
                        TooltipUI.Instance.Show("Cannot afford " + activeBuildingType.GetConstructionResourceCostString(), new TooltipUI.TooltipTimer { timer = 1.5f });
                    }
                }
                else {
                    TooltipUI.Instance.Show(errorMesssage, new TooltipUI.TooltipTimer { timer = 1.5f });
                }
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(
            this,
            new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = this.activeBuildingType }
        );
    }

    public BuildingTypeSO GetActiveBuildingType() { 
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage) {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;

        if (!isAreaClear) {
            errorMessage = "Cell is occupied!";

            return false;
        }

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();

            if (buildingTypeHolder != null) {
                if (buildingTypeHolder.buildingType == buildingType) {
                    errorMessage = "Close to another building of same type!";
                    return false;
                }
            }
        }

        float maxConstuctionRadius = 10f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstuctionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();

            if (buildingTypeHolder != null) {
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "Too far from other buildings!";
        return false;
    }

}
