using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour{
    public static Inventory instance { get; private set; }

    [Header("References")]
    [SerializeField] InventoryUI ui;
    [SerializeField] AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField] GameObject droppedItemPrefab;

    [Header("Audio")]
    [SerializeField] AudioClip pickUpItem;
    [SerializeField] AudioClip dropItem;

    public Dictionary<string, Item> inventory = new();

    private void Awake(){
        instance = this;
    }


    private void Start(){
        InteractableObject.OnInteract += OnInteract;
    }

    public void OnInteract(InteractableObject other){
        

        if (other.CompareTag("DroppedItem")){
            if (other.pickedUp){
                return;
            }
            other.pickedUp = true;
            AddItem(other.item);
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickUpItem);
        }
    }

    public void AddItem(Item item){
        audioSource.PlayOneShot(pickUpItem);
        item.id = Guid.NewGuid().ToString();
        inventory.Add(item.id, item);
        ui.AddUIItem(item.id, item);
    }

    public void DropItem(string inventoryId){
        //var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<InteractableObject>();
        var item = inventory.GetValueOrDefault(inventoryId);
        inventory.Remove(inventoryId);
        ui.RemoveUIItem(inventoryId);
        audioSource.PlayOneShot(dropItem);
    }




}

