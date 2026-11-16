using UnityEngine;

public class ScrapSpawner : MonoBehaviour{

    private int rand;
    private GenerationManager generationManager;
    public GameObject[] Scraps;

    GameObject ScrapParent;

    private float time = 0;
    private float respawnTime = 15f;

    void Start(){  
        Spawn();
    }

    private void Update(){
        time += Time.deltaTime;

        if (time >= respawnTime){
            Spawn();
        }
    }

    void Spawn(){
        respawnTime = Random.Range(15, 35);
        time = 0;
        ScrapParent = GameObject.Find("SCRAPPARENT");
        generationManager = GameObject.FindGameObjectWithTag("Rooms").GetComponent<GenerationManager>();
        rand = Random.Range(0, Scraps.Length);

        float randRange = Random.Range(-2.5f, 2.5f);
        Vector3 randomPos = new Vector3(randRange, 0, randRange);

        Instantiate(Scraps[rand], transform.position + randomPos, Quaternion.identity, ScrapParent.transform);
    }

}
