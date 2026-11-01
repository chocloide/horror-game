using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InteractableController : MonoBehaviour{

    public float Distance = 5.0f;
    TaskObject current;
    [SerializeField] TextMeshProUGUI InteractionText;
    [SerializeField] CameraController Camera;

    void Update(){
        Ray r = new Ray(Camera.transform.position, Camera.transform.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, Distance) && hitInfo.collider.gameObject.TryGetComponent(out TaskObject interactObj)){
            if (current != interactObj){
                Clear();
                current = interactObj;
                current.text = InteractionText;
                current.OnHover();
            }

            if (Input.GetKeyDown(KeyCode.E)){
                current.Interact();
            }
        }
        else{
            Clear();
        }
    }

    void Clear(){
        if(current != null){
            current.OnHoverExit();
        }
        current = null;
    }

}



