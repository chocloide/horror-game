using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IInteractable{
    public void Interact();
    public void OnHover();
    public void OnHoverExit();
}

public class InteractableController : MonoBehaviour{

    private GameObject Camera;
    public float Distance = 5.0f;
    IInteractable current;

    private void Start(){
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update(){
        Ray r = new Ray(Camera.transform.position, Camera.transform.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, Distance) && hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)){
            if (current != interactObj){
                Clear();
                current = interactObj;
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



