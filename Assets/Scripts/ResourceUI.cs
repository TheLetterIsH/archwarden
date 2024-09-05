using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour {

    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    private void Awake() {
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceElementTemplate = transform.Find("Resource Element Template");
        resourceElementTemplate.gameObject.SetActive(false);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            Transform resourceElementTransform = Instantiate(resourceElementTemplate, transform);
            resourceElementTransform.gameObject.SetActive(true);

            resourceElementTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeTransformDictionary[resourceType] = resourceElementTransform;
        }
    }

    private void Start() {
        ResourceManager.Instance.OnResourceAmountChanged += OnResourceAmountChanged;

        UpdateResourceAmount();
    }

    private void OnResourceAmountChanged(object sender, System.EventArgs e) {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount() {
        foreach (ResourceTypeSO resourceType in resourceTypeTransformDictionary.Keys) {
            Transform resourceElementTransform = resourceTypeTransformDictionary[resourceType];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceElementTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
