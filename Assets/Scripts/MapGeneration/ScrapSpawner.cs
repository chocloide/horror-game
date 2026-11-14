using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour{

    private int rand;
    private GenerationManager generationManager;
    public GameObject[] Scraps;


    void Start(){
        generationManager = GameObject.FindGameObjectWithTag("Rooms").GetComponent<GenerationManager>();
        rand = Random.Range(0, Scraps.Length);

        Instantiate(Scraps[rand], transform.position, Quaternion.identity);
        
    }

}
