using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_movement : MonoBehaviour
{
    private NavMeshAgent movement;
    public GameObject player;
    public float move_speed;
    public float walk_range = 10;
    public float sprint_range = 20;
    public float sight_range = 20;
    public float angle = 60;
    public LayerMask PlayerMask;
    public float attack_range = 1.2f;
    private int current_point;
    public float max_time = 5;
    [SerializeField]List<Transform> PatrolPoints =  new List<Transform>();

    private void Awake()
    {
        movement = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        bool c1 = Walk_Check();
        bool c2 = FOV_check();
        if (c1 || c2)
        {
            Follow_player();
        }
        else if (!(movement.hasPath) || movement.remainingDistance <= attack_range)
        {
            StartCoroutine(Patrol());
        }
    }

    private bool FOV_check()
    {
        Collider[] present = Physics.OverlapSphere(transform.position, sight_range, PlayerMask);
        if (present.Length != 0)
        {
            if (Vector3.Angle(transform.position, (present[0].transform.position - transform.position).normalized) < angle)
            {
                Debug.Log("FOV check");
                return true;
            }
        }
        return false;

    }
    private bool Walk_Check()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < walk_range)
        {
            Debug.Log("Walk check");
            return true;
        }
        return false;
        

    }
    private void Follow_player()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > attack_range)
        {
            movement.speed = move_speed;
            movement.destination = player.transform.position;
        }
        else
        {
            Attack_Player();
        }
    }
    private void Attack_Player()
    {
        movement.ResetPath();
    }

    IEnumerator Patrol()
    {
        movement.ResetPath();
        ShiftMovement();
        yield return new WaitForSeconds(2);
    }
    void ShiftMovement()
    {
        if (PatrolPoints.Count == 0)
        {
            return;
        }
        movement.SetDestination(PatrolPoints[current_point].position);
        Debug.Log(current_point);
        current_point = (current_point + 1) % PatrolPoints.Count;
    }
}
//            if (Vector3.Distance(transform.position, player.transform.position) > attack_range)
//            {
//                movement.speed = move_speed;
//                time_counter += Time.deltaTime;
//                back = transform.position;
//                movement.destination = player.transform.position;
//            }
//            else
//            {
//                if (chance > 3)
//                {
//                    movement.speed = sprint_speed;
//                    movement.destination = player.transform.position - (Vector3.one);
//                    if (Vector3.Distance(transform.position, player.transform.position) < 1.3f)
//                    {
//                        chance = Random.Range(0, 10);
//                        movement.destination = back;
//                    }
//                }
//                else
//                {
//                    time_counter += Time.deltaTime;
//                    if (time_counter > max_time)
//                    {
//                        chance = Random.Range(0, 10);
//                        time_counter = 0;
//                    }
//                    movement.destination = back;
//                }
//            }
//        
//        else
//        {
//            back = transform.position;
//            movement.speed = move_speed;
//            StartCoroutine(OutOfRange());
//        }
//
//    IEnumerator OutOfRange()
//    {
//        while (InRange)
//        {
//            movement.destination = back + (Vector3.left * 3);
//            yield return new WaitForSeconds(2);
//            movement.destination = back + (Vector3.right * 3);
//            InRange = false;
//        }
//    }