using UnityEngine;


public class InteractableController : MonoBehaviour{

    public float Distance = 5.0f;
    InteractableObject current;

    [SerializeField] CameraController Camera;

    void Update(){
        Ray r = new Ray(Camera.transform.position, Camera.transform.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, Distance)){
            // si el raycast collisiona intenta obtener el componente InteractableObject
            if (hitInfo.collider.gameObject.TryGetComponent(out InteractableObject interactObj)){
                if (current != interactObj){
                    Clear();
                    current = interactObj;
                    current.OnHover();
                }

                if (Input.GetKeyDown(KeyCode.E) && current){
                    current.Interact();
                    Clear();
                }

            }
            else{
                Clear();
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



