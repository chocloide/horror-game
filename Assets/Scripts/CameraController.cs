using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Sensitivity = 1f;
    public float smoothness = 1f;

    Vector2 smoothRotation = Vector2.zero;
    Vector2 targetRotation = Vector2.zero;

    public PlayerController player;
    public GameObject Head;
    public GameObject Handler;

    public Vector2 amplitude = new Vector2(0.0015f, 0.0015f);
    public float frequency = 1.0f;
    private float ToggleSpeed = 1.0f;

    private Vector3 start_pos;
    


    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake(){
        start_pos = Handler.transform.localPosition;
    }

    private void PlayMotion(Vector3 motion){

        Handler.transform.localPosition += motion;
    }

    private void CheckMotion(){
        float speed = new Vector3(player.Rigid.velocity.x, 0, player.Rigid.velocity.z).magnitude;

        if (speed < ToggleSpeed) return;
        PlayMotion(footStepMotion());
    }

    public void ChangeFrequency(float newfrequency){
        frequency = newfrequency;
    }

    private Vector3 footStepMotion(){
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude.y;
        pos.x += Mathf.Cos((frequency / 2) * Time.time) * amplitude.x;
        return pos;
    }
    private void ResetPosition(){
        if (Handler.transform.localPosition == start_pos) return;
        Handler.transform.localPosition = Vector3.Lerp(Handler.transform.localPosition, start_pos, 3 * Time.deltaTime);
    }

    void LateUpdate(){
        float mouseX = Input.GetAxisRaw("Mouse Y") * Sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse X") * Sensitivity;

        targetRotation.x -= mouseX;
        targetRotation.x = Mathf.Clamp(targetRotation.x, -90f, 90f);
        targetRotation.y += mouseY;

        smoothRotation.x = Mathf.Lerp(smoothRotation.x, targetRotation.x, smoothness);
        smoothRotation.y = Mathf.Lerp(smoothRotation.y, targetRotation.y, smoothness);

        //transform.rotation = Quaternion.Euler(smoothRotation.x, smoothRotation.y, 0);
        player.transform.localRotation = Quaternion.Euler(0, smoothRotation.y, 0);
        Head.transform.localRotation = Quaternion.Euler(smoothRotation.x, 0, 0);

        CheckMotion();
        ResetPosition();
    }
}



