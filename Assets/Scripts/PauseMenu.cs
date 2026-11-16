using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public static PauseMenu instance { get; private set; }
    public bool paused = false;

    [SerializeField] Canvas PauseCanvas;

    public static event Action<bool> OnPause;
    public static event Action OnMainMenu;

    private void Awake()
    {
        OnPause = null;
        OnMainMenu = null;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            OnPause.Invoke(paused);

            CheckPause();
        }
    }


    void CheckPause(){
        if (paused){
            PauseCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else{
            Cursor.lockState = CursorLockMode.Locked;
            PauseCanvas.enabled = false;
        }
    }

    public void OnMainMenuPress(){
        OnMainMenu.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnResumePress(){
        paused = !paused;
        OnPause.Invoke(paused);

        CheckPause();
    }


}
