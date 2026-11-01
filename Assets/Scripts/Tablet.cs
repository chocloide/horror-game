using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Tablet : MonoBehaviour
{
    [SerializeField] Canvas UI;

    public TaskObject obj = null;
    [SerializeField] Camera SceenCamera;

    private void Start(){
        //Test de tareas
        TaskManager.instance.NewTaskObject("Toca la Pelota 2 veces", obj, 2);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){

            UI.enabled = !UI.enabled;
        }


    }
}

