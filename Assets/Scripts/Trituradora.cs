using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.ParticleSystem;

public class Trituradora : MonoBehaviour
{

    float progress = 1;
    public float MaxProgress = 1;
    [SerializeField] Image ProgressBar;

    AudioManager audioManager;

    private InteractableObject interactableComponent;
    private Animation Anim;
    [SerializeField] ParticleSystem particle;

    List<Item> listOfScrap;

    void Start(){
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        Anim = GetComponent<Animation>();
        interactableComponent = gameObject.GetComponent<InteractableObject>();
        InteractableObject.OnInteract += UpdateProgress;

        PauseMenu.OnPause += OnGamePaused;
        PauseMenu.OnMainMenu += OnMainMenu;
    }

    void Update(){
        ProgressBar.fillAmount = progress / MaxProgress;
        progress = Mathf.Max(progress - (0.025f * Time.deltaTime), 0);

    }

    public void OnMainMenu(){
        PauseMenu.OnPause -= OnGamePaused;
    }
    public void OnGamePaused(bool pause){
        this.enabled = !pause;
    }


    void UpdateProgress(InteractableObject obj){
        if (obj != interactableComponent || Inventory.instance.inventory.Count == 0) return;

        audioManager.PlayAudio(0); List<string> keysToRemove = new List<string>();
        
        foreach (KeyValuePair<string, Item> item in Inventory.instance.inventory){
            
            keysToRemove.Add(item.Key);
            particle.Play();
            //listOfScrap.Add(item.Value);
            Anim.Play("TrituradoraCrush");
        }

        foreach (var key in keysToRemove){
            Inventory.instance.DropItem(key);
            progress += 0.1f;
        }

    }
}