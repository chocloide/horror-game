using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    [SerializeField] Canvas MainMenuCanvas;

    public void OnPlayPress(){
        SceneManager.LoadScene("TestScene");
    }


    public void OnExitPress(){
        Application.Quit();
    }

}
