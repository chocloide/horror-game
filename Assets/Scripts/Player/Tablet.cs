using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;


public class Tablet : MonoBehaviour
{
    [SerializeField] List<Canvas> Apps = new List<Canvas>();

    [SerializeField] Material ScreenOff;
    [SerializeField] Material ScreenOn;

    [SerializeField] GameObject Screen;

    public InteractableObject obj = null;

    int index = 0;

    [SerializeField] Camera SceenCamera;
    Animation Anim;
    Canvas currentApp;
    Canvas previousApp;

    bool seeing = false;

    private void Start(){
        //Test de tareas
        Anim = GetComponent<Animation>();

        currentApp = Apps[0];
        previousApp = Apps[0];
        //TaskManager.instance.NewTaskObject("Toca la Pelota 2 veces", obj, 2);
    }

    void Update(){
        ManageStates();
        if (Input.GetKeyDown(KeyCode.Tab)){
            seeing = !seeing;

            if (seeing){
                Anim.Play("TabletSeeing");
            }
            else
            {
                Anim.Play("TabletNormal");
            }

        }

    }

    void ManageStates(){
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f){
            index++;

            if (index > Apps.Count - 1){
                index = 0;
            }
            previousApp = currentApp;
            currentApp = Apps[index];

            previousApp.enabled = false;
            currentApp.enabled = true;

        }
        else if (scroll < 0f){
            index--;
            if (index < 0){
                index = Apps.Count - 1;
            }

            previousApp = currentApp;
            currentApp = Apps[index];

            previousApp.enabled = false;
            currentApp.enabled = true;
        }
    }


}


