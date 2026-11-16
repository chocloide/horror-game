using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour{
    [Header("References")]
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Button button;

    public void Initialize(string InventoryId, Item item, Action<string> removeItemAction){
        image.sprite = item.icon;
        textMesh.text = item.name;
        transform.localScale = Vector3.one;
    }

    private void OnDestroy(){
        button.onClick.RemoveAllListeners();
    }

}
