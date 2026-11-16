using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour{

    public float maxHealth = 75;
    public float health;

    public static event Action OnPlayerDead;
    public static event Action OnPlayerHurt;
    [SerializeField] Rigidbody rb;

    private void Awake(){
        OnPlayerHurt = null;
        OnPlayerDead = null;

        health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision){

        if (collision.collider.gameObject.tag == "Enemigo"){
            rb.AddForce(Vector3.back * 500, ForceMode.Force);
            OnPlayerHurt.Invoke();
            health -= 1;

            if (health <= 0){
                OnPlayerDead.Invoke();
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
