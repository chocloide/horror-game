using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Tablet : MonoBehaviour
{
    private TaskManager taskManager;
    [SerializeField] Canvas UI;

    public CheckArea CheckArea = null;
    public TaskObject obj = null;

    private void Start(){
        taskManager = FindObjectOfType<TaskManager>();
        //Test de tareas
        taskManager.NewTaskObject("Toca la esfera", obj, 2);
        taskManager.NewTaskPlace("Anda al cubo", CheckArea);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)){
            //UI.enabled = !UI.enabled;
        }
    }
}

