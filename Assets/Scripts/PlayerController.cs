using UnityEngine;

public class PlayerController : MonoBehaviour{
    Vector2 input = Vector2.zero;
    Vector3 Direction = Vector3.zero;

    public int walkSpeed = 1;
    public int runSpeed = 3;

    public float maxEnergy = 10;
    public float energy = 10;
    bool can_run = true;

    public Rigidbody Rigid;
    public CameraController Camera;


    void Start(){
        energy = maxEnergy;
        Rigid = GetComponent<Rigidbody>();
    }

    void Update(){
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate(){
        Direction = transform.forward * input.y + transform.right * input.x;

        if (energy >= maxEnergy){
            can_run = true;
        }

        else if (energy <= 0){
            can_run = false;
        }

        if (Input.GetButton("Run") && can_run && input != Vector2.zero){
            energy = Mathf.Max(energy - Time.deltaTime, 0);
            Camera.ChangeFrequency(20f);
            Rigid.AddForce(new Vector3(Direction.x * runSpeed, 0, Direction.z * runSpeed), ForceMode.VelocityChange);
        }

        else{
            energy = Mathf.Min(energy + Time.deltaTime, maxEnergy);
            Camera.ChangeFrequency(10f);
            Rigid.AddForce(new Vector3(Direction.x * walkSpeed, 0, Direction.z * walkSpeed), ForceMode.VelocityChange);
        }
    }
}
