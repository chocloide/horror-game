using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour{

    public float maxHealth = 75;
    public float health;

    public static event Action OnPlayerDead;
    [SerializeField] Rigidbody rb;

    private void Awake(){
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision){
        rb.AddForce(Vector3.back * 500, ForceMode.Force);

        if (collision.collider.gameObject.tag == "Enemigo"){
            health -= 5;

            if (health <= 0){
                OnPlayerDead.Invoke();
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
