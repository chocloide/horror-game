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


    public enum InteractableType {Default, Object}

    public InteractableType type = InteractableType.Default;

    [Header("Settings")]
    [SerializeField] bool autoStart;

    [SerializeField] float enablePickUpDelay = 3.0f;

    [Header("State")]
    public Item item;
    public bool pickedUp = false;


    private void Start(){
        NameLabel = GameObject.Find("TextName").GetComponent<TextMeshProUGUI>();
        DescriptionLabel = GameObject.Find("TextDescription").GetComponent<TextMeshProUGUI>();
    }

    public void Interact(){
        if (can_interact){
            OnInteract.Invoke(this);
        }
    }

    public void OnHover(){
        NameLabel.text = objName;
        DescriptionLabel.text = description;
    }
    public void OnHoverExit(){
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
