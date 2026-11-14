using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawPoint : MonoBehaviour{
    public int openingDirection;
    //1 - necesita Bottom room
    //2 - necesita Top rooom
    //3 - necesita Left room
    //4 - necesita Right room

    private GenerationManager templates;
    private int rand;
    private bool spawned =false;

    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<GenerationManager>();
        Invoke("SpawnRooms", 0.1f);
    }

    void SpawnRooms(){
        if (spawned == false){
            if (openingDirection == 1)
            {//BOTTOM
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }

            else if (openingDirection == 2)
            {//TOP
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }

            else if (openingDirection == 3)
            {//LEFT
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {//RIGHT
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }

            spawned = true;
        }

    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("RoomSpawnPoint")){
            if (other.GetComponent<RoomSpawPoint>().spawned == false && spawned == false){
                Instantiate(templates.CloseRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
