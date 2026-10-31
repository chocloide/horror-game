using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TaskObject : MonoBehaviour, IInteractable{

    public string Name;
    public KeyCode input;
    [SerializeField] TextMeshProUGUI text;
    public UnityEvent<TaskObject> onInteract;

    public void Interact(){
        onInteract.Invoke(this);
    }

    public void OnHover(){
        text.text = Name;
    }
    public void OnHoverExit(){
        text.text = " ";
    }
}
