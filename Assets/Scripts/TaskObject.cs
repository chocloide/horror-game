using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TaskObject : MonoBehaviour{

    public string Name;
    public KeyCode input;

    public TextMeshProUGUI text;

    public static event Action<TaskObject> OnInteract; 
    // Los Action son como "señales" que permiten que otros scripts
    // ejecuten metodos sin que dependan directamente entre sí 

    public void Interact(){
        OnInteract(this);
    }

    public void OnHover(){
        text.text = Name;
    }
    public void OnHoverExit(){
        text.text = " ";
    }
}
