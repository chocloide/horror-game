using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Trituradora : MonoBehaviour
{

    float progress = 0;
    public float progressNeeded = 1;
    [SerializeField] Image ProgressBar;

    private InteractableObject interactableComponent;
    private Animation Anim;

    List<Item> listOfScrap;

    void Start(){
        Anim = GetComponent<Animation>();
        interactableComponent = gameObject.GetComponent<InteractableObject>();
        InteractableObject.OnInteract += UpdateProgress;
    }

    void UpdateProgress(InteractableObject obj){
        if (obj != interactableComponent) return;
        List<string> keysToRemove = new List<string>();
        
        foreach (KeyValuePair<string, Item> item in Inventory.instance.inventory){
            keysToRemove.Add(item.Key);
            //listOfScrap.Add(item.Value);
            //Anim.Play("TrituradoraCrush");
        }

        foreach (var key in keysToRemove){
            Inventory.instance.DropItem(key);
            progress += 0.1f;
        }

        ProgressBar.fillAmount = progress / progressNeeded;
    }
}