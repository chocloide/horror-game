using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour{

    public bool can_interact = true;

    public string objName;
    public string description;

    public static event Action<InteractableObject> OnInteract;
    // Los Action son como "señales" que permiten que otros scripts
    // ejecuten metodos sin que dependan directamente entre sí 

    private TextMeshProUGUI NameLabel;
    private TextMeshProUGUI DescriptionLabel;
    private Material outline;
    
    public AudioManager audioManager;

    public bool canOutline = true;
    public bool PlayAudio = true;
    public int audioId = 0;
    public enum InteractableType {Default, Object}

    public InteractableType type = InteractableType.Default;

    [Header("Settings")]
    [SerializeField] bool autoStart;

    [SerializeField] float enablePickUpDelay = 3.0f;

    [Header("State")]
    public Item item;
    public bool pickedUp = false;


    private void Start(){
        if (canOutline){
            outline = GetComponent<Renderer>().materials[1];
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        NameLabel = GameObject.Find("TextName").GetComponent<TextMeshProUGUI>();
        DescriptionLabel = GameObject.Find("TextDescription").GetComponent<TextMeshProUGUI>();
    }

    public void Interact(){
        if (can_interact){
            if (PlayAudio) audioManager.PlayAudio(audioId);
            OnInteract.Invoke(this);
        }
    }

    public void OnHover(){
        if (canOutline){
            outline.SetFloat("_Active", 1);
        }
        NameLabel.text = objName;
        DescriptionLabel.text = description;
    }

    public void OnHoverExit(){
        if (canOutline){
            outline.SetFloat("_Active", 0);
        }
        NameLabel.text = " ";
        DescriptionLabel.text = " ";
    }

    public void Initialize(Item item){
        this.item = item;
        var droppedItem = Instantiate(item.prefab, transform);
        droppedItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        StartCoroutine(enablePickUp(enablePickUpDelay));
    }

    IEnumerator enablePickUp(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
    }


}
