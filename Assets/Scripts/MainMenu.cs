using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    [SerializeField] Canvas OptionsCanvas;
    [SerializeField] Canvas MainMenuCanvas;


    bool SeeingOptions = false;

    public void OnPlayPress(){
        SceneManager.LoadScene("TestScene");
    }

    public void OnOptionsPress(){
        SeeingOptions = !SeeingOptions;

        OptionsCanvas.enabled = SeeingOptions;
        MainMenuCanvas.enabled = !SeeingOptions;
    }

    public void OnExitPress(){
        Application.Quit();
    }

}
