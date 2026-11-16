using Unity.AI.Navigation;
using UnityEngine;

public class GenerationManager : MonoBehaviour{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject CloseRoom;

    public int maxScrap;
    public int totalScrap = 0;

    public NavMeshSurface navMesh;
    public GameObject Enemy;

    private void Start(){
        Invoke("BakeMesh", 2f);
        Invoke("GenerateEnemy", 5f);
    }

    void BakeMesh(){
        navMesh.BuildNavMesh();
    }

    void GenerateEnemy(){
        Instantiate(Enemy,new Vector3(5,0,5), Quaternion.identity);

    }

}
