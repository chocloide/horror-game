using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour{

    [SerializeField] TextMeshProUGUI bestTime;

    private void Start(){
        Cursor.lockState = CursorLockMode.None;
        float time = SaveSystem.instance.GetBestTime();

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float mil_seconds = Mathf.FloorToInt((time % 1) * 100);

        bestTime.text = "Best Time \n";
        bestTime.text += string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, mil_seconds);
    }

    public void OnRetryPress(){
        SceneManager.LoadScene("GameScene");
    }


    public void OnMainMenuPress(){
        SceneManager.LoadScene("MainMenu");
    }

}
