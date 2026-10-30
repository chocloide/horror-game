using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Tablet : MonoBehaviour
{
    [SerializeField] TaskManager taskManager;
    [SerializeField] Canvas UI;

    public CheckArea CheckArea = null;


    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            UI.enabled = !UI.enabled;
        }
    }
}

