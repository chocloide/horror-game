using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Trituradora : MonoBehaviour{

    float progress = 1;
    public float MaxProgress = 1;
    [SerializeField] Image ProgressBar;

    AudioManager audioManager;

    private InteractableObject interactableComponent;
    private Animation Anim;
    [SerializeField] ParticleSystem particle;

    public static event Action OnEnergyOut;
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
        progress = Mathf.Max(progress - (0.022f * Time.deltaTime), 0);

        if (progress <= 0){
            OnEnergyOut.Invoke();
            SceneManager.LoadScene("GameOver");
        }

    }

    public void OnMainMenu(){
        PauseMenu.OnPause -= OnGamePaused;
    }
    public void OnGamePaused(bool pause){
        this.enabled = !pause;
    }

    void UpdateProgress(InteractableObject obj){
        if (obj != interactableComponent || Inventory.instance.inventory.Count == 0) return;

        audioManager.PlayAudio(0); 
        List<string> keysToRemove = new List<string>();
        
        foreach (KeyValuePair<string, Item> item in Inventory.instance.inventory){
            
            keysToRemove.Add(item.Key);
            particle.Play();
            Anim.Play("TrituradoraCrush");
        }

        foreach (var key in keysToRemove){
            Inventory.instance.DropItem(key);
            progress =  Mathf.Min(progress + 0.1f, MaxProgress);
        }

    }
}