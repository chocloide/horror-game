using UnityEngine;

public class AudioManager : MonoBehaviour{

    public AudioClip[] clips;
    AudioSource audioSource;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int audioIndex){
        audioSource.PlayOneShot(clips[audioIndex]);
        audioSource.pitch = Random.Range(0.8f, 1.2f);

    }

}
