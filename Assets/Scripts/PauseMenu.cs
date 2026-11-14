using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{
    public bool paused = false;

    [SerializeField] Canvas PauseCanvas;
    [SerializeField] Canvas OptionsCanvas;

    public static event Action<bool> OnPause;
    public static event Action OnMainMenu;
    bool SeeingOptions = false;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            OnPause.Invoke(paused);

            CheckPause();
        }
    }

    public void OnOptionsPress(){
        SeeingOptions = !SeeingOptions;

        OptionsCanvas.enabled = SeeingOptions;
        PauseCanvas.enabled = !SeeingOptions;
    }

    void CheckPause(){
        if (paused){
            PauseCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else{
            Cursor.lockState = CursorLockMode.Locked;
            PauseCanvas.enabled = false;
            OptionsCanvas.enabled = false;
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
