using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField] private Sprite pointerSprite;
    [SerializeField] private float buttonWidth;
    [SerializeField] private float horizontalSpacing;

    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDictionary;
    private Transform pointerButton;

    private void Awake() {
        Transform buildingTypeButtonTemplate = transform.Find("Building Type Button Template");
        buildingTypeButtonTemplate.gameObject.SetActive(false);

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        // Pointer Button
        pointerButton = Instantiate(buildingTypeButtonTemplate, transform);
        pointerButton.gameObject.SetActive(true);

        pointerButton.Find("Building Image").GetComponent<Image>().sprite = pointerSprite;

        pointerButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        foreach (BuildingTypeSO buildingType in buildingTypeList.list) { 
            Transform buildingTypeButtonTransform = Instantiate(buildingTypeButtonTemplate, transform);
            buildingTypeButtonTransform.gameObject.SetActive(true);

            buildingTypeButtonTransform.Find("Building Image").GetComponent<Image>().sprite = buildingType.sprite;

            buildingTypeButtonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            buildingTypeTransformDictionary[buildingType] = buildingTypeButtonTransform;
        }

        int numberOfButtons = buildingTypeList.list.Count + 1;
        float xOffset = (
             - buttonWidth * numberOfButtons
             - horizontalSpacing * (numberOfButtons - 1)
        ) / 2;
        
        float y = transform.GetComponent<RectTransform>().anchoredPosition.y;

        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(xOffset, y);
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e) {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        pointerButton.Find("Button Selected Outline Image").gameObject.SetActive(false);

        foreach (BuildingTypeSO buildingType in buildingTypeTransformDictionary.Keys) {
            Transform buildingTypeButtonTransform = buildingTypeTransformDictionary[buildingType];
            buildingTypeButtonTransform.Find("Button Selected Outline Image").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (activeBuildingType == null) {
            pointerButton.Find("Button Selected Outline Image").gameObject.SetActive(true);
        }
        else {
            buildingTypeTransformDictionary[activeBuildingType].Find("Button Selected Outline Image").gameObject.SetActive(true);
        }
    }

}
