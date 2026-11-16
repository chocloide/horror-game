using UnityEngine;

public class PlayerController : MonoBehaviour{
    Vector2 input = Vector2.zero;
    Vector3 Direction = Vector3.zero;

    public int walkSpeed = 1;
    public int runSpeed = 3;
    public float upgradeSpeed = 0;

    public float maxEnergy = 10;
    public float energy = 10;

    bool can_run = true;

    public Rigidbody Rigid;
    [SerializeField] CameraController Camera;
    [SerializeField] AudioClip FootStep;
    [SerializeField] AudioSource audioSource;
    void Start(){
        energy = maxEnergy;
        Rigid = GetComponent<Rigidbody>();
        PauseMenu.OnPause += OnGamePaused;
        PauseMenu.OnMainMenu += OnMainMenu;
    }

    void Update(){
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void OnMainMenu(){
        PauseMenu.OnPause -= OnGamePaused;
    }
    public void OnGamePaused(bool pause){
        this.enabled = !pause;
        Rigid.freezeRotation = pause;
    }

    void FixedUpdate(){
        Direction = transform.forward * input.y + transform.right * input.x;

        if (energy >= maxEnergy){
            can_run = true;
        }

        else if (energy <= 0){
            can_run = false;
        }

        if (input != Vector2.zero){
            if (audioSource.isPlaying == false) audioSource.PlayOneShot(FootStep);

            if (Input.GetButton("Run") && can_run){
                audioSource.pitch = 1.4f;
                energy = Mathf.Max(energy - Time.deltaTime, 0);
                Camera.ChangeFrequency(20f);
                Rigid.AddForce(new Vector3(Direction.x * runSpeed + upgradeSpeed, 0, Direction.z * runSpeed), ForceMode.VelocityChange);
            }

            else{
                audioSource.pitch = 1f;
                energy = Mathf.Min(energy + 5 * Time.deltaTime, maxEnergy);
                Camera.ChangeFrequency(10f);
                Rigid.AddForce(new Vector3(Direction.x * walkSpeed, 0, Direction.z * walkSpeed), ForceMode.VelocityChange);
            }
        }

        else{
            audioSource.Stop();
            energy = Mathf.Min(energy + 5 * Time.deltaTime, maxEnergy);
        }
    }
}
