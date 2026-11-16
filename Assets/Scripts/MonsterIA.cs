using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterIA : MonoBehaviour
{
    enum States { Patrol, Sus, Chase};

    States currentState = States.Patrol;

    NavMeshAgent agent;
    List<GameObject> PointsList = new List<GameObject>();

    int index = 0;
    private PlayerController Player;
    
    Vector3 CurrentPosition = Vector3.zero;
    Vector3 SusPosition = Vector3.zero;
    float distanceLeft = 0f;
    float distancePlayer = 0f;

    public float sus = 0f;

    float timeSearching = 5f;
    float time = 5f;

    public float patrolSpeed = 3.8f;
    public float chaseSpeed = 5.5f;

    public float radius;
    [Range(0, 360)]
    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;


    void Start(){
        GetChildPoints();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        CurrentPosition = PointsList[0].transform.position;
        StartCoroutine(FOVRoutine());


        PauseMenu.OnPause += OnGamePaused;
        PauseMenu.OnMainMenu += OnMainMenu;
    }

    void Update(){
        distancePlayer = Vector3.Distance(transform.position, Player.transform.position);

        switch (currentState){
            case States.Patrol:
                PatrolState();
                break;

            case States.Sus: 
                SusState(); 
                break;

            case States.Chase:
                ChaseState();
                break;
        }
    }
    public void OnMainMenu(){
        PauseMenu.OnPause -= OnGamePaused;
    }

    public void OnGamePaused(bool pause){
        this.enabled = !pause;
        agent.enabled = !pause;
    }

    private void PatrolState(){
        distanceLeft = Vector3.Distance(CurrentPosition, transform.position);
        
        agent.SetDestination(CurrentPosition);
        sus = Mathf.Min(sus + 1 * Time.deltaTime, 50);

        // cuando el enemigo esta suficiente cerca del punto va al siguiente
        if (distanceLeft < 2){
            index = (index + 1) % PointsList.Count;
            CurrentPosition = PointsList[index].transform.position;
        }

        // cuando la variable sus llega a 25 o vee al jugador, va hacia el punto mas cerla a la posicion del jugador
        else if (sus >= 25 || canSeePlayer ){
            SusPosition = GetClosePoint(Player.transform.position).position; 
            currentState = States.Sus;
            time = timeSearching;
        }
    }

    private void SusState(){
        sus = Mathf.Min(sus + 1 * Time.deltaTime, 50);
        agent.SetDestination(SusPosition);
        time -= Time.deltaTime;

        // cuando la variable sus es mayor o igual 45 o vee al jugador, Empieza a persguir al jugador
        if (sus >= 45 || canSeePlayer){
            currentState = States.Chase;
            agent.speed = chaseSpeed;
            sus = 50;
        }

        else if (time <= 0){
            currentState = States.Patrol;
        }

    }

    private void ChaseState(){
        agent.SetDestination(Player.transform.position);
        sus = Mathf.Max(sus - 5 * Time.deltaTime, 0);

        // Cuando la variable sus llega a 0 o no vee al jugador vuele a patrullar al punto mas cercano
        if (sus <= 0 & !canSeePlayer){
            transform.position = PointsList[Random.Range(0,PointsList.Count)].transform.position;
            CurrentPosition = PointsList[Random.Range(0, PointsList.Count)].transform.position;
            agent.speed = patrolSpeed;
            currentState = States.Patrol;
        }
    }

    private void GetChildPoints(){
        GameObject[] transform = GameObject.FindGameObjectsWithTag("PatrolPoint");
        
        foreach (GameObject child in transform){
            PointsList.Add(child.gameObject);
        }
    }

    private Transform GetClosePoint(Vector3 pos){
        int i = 0;
        foreach (GameObject point in PointsList){
            i++;
            float DistanceTo = Vector3.Distance(pos, point.transform.position);

            if (DistanceTo < 20 & DistanceTo > 0){
                index = i;
                return point.transform; 

            }
        }
        return PointsList[0].transform; ;
    }

    public void AddSus(float amount){
        if (currentState != States.Chase & (distancePlayer < 25 & distancePlayer > 0)){
            sus = Mathf.Min(sus + amount, 50);
        }
        
    }

    private IEnumerator FOVRoutine(){
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true){
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck(){
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0){
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //si el jugador esta en el angulo de vision
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2){
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;

                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }
        else if (canSeePlayer) canSeePlayer = false;
    }
}



