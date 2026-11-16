using System.Collections.Generic;
using UnityEngine;

public class UpgradeMachine : MonoBehaviour{
    
    HealthSystem healthSystem;
    PlayerController player;
    private InteractableObject interactableComponent;
    [SerializeField] AudioSource Audio;

    public int scrapNeeded = 0;
    enum UpgradeType{Health,Stamina,Speed};

    [SerializeField] UpgradeType upgrade = UpgradeType.Health;

    private void Start(){
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        interactableComponent = gameObject.GetComponent<InteractableObject>();

        InteractableObject.OnInteract += OnInteract;
    }


    void OnInteract(InteractableObject obj){
        if (obj != interactableComponent || Inventory.instance.inventory.Count < scrapNeeded) return;

        List<string> keysToRemove = new List<string>();
        Audio.Play();
        int i = 0;

        foreach (KeyValuePair<string, Item> item in Inventory.instance.inventory){
            i++;
            keysToRemove.Add(item.Key);

            if (i == scrapNeeded) break;
        }

        foreach (var key in keysToRemove){
            Inventory.instance.DropItem(key);
        }

        if (upgrade == UpgradeType.Health){
            healthSystem.health = Mathf.Min(healthSystem.health + 1, healthSystem.maxHealth);
        }

        else if (upgrade == UpgradeType.Stamina){
            player.maxEnergy += 5;
        }

        else if (upgrade == UpgradeType.Speed){
            player.upgradeSpeed += 0.25f;
        }

    }



}
