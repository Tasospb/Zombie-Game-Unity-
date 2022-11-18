// This is the Brain for the EXO Npc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class ZombieAI : MonoBehaviour
{

    Animator anim;      // the animator
    NavMeshAgent na;    // the nav mesh agent

    public float walkingSpeed = 4f;
    public float chaseSpeed = 7f;
    public float countdowntimer = 0f;
    public float attackSpeed = 1f;

    public Transform target;        // the target (which will be the player)

    Spawner spawn;

    // Use this for initialization
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect the player
        float enemydist = Vector3.Distance(target.transform.position,
            transform.position);    

        //enemydist = 10000; //??

        anim.SetFloat("EnemyDist", enemydist);

        // Distance to the home base of the Hammer Warrior NPC
        //float basedist = Vector3.Distance(targetbase.transform.position,
        //    transform.position);

        //anim.SetFloat("BaseDist", basedist);

        // Determine which state Hammer Warrior is in
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);

        countdowntimer -= Time.deltaTime;

        // If Zombie is in Attack state
        if (asi.IsName("Boxing"))
        {
            na.isStopped = true;
            na.velocity = Vector3.zero;
            Hit();
        }

        // If Zombie is in run state
        if (asi.IsName("Walking"))
        {
            na.SetDestination(target.position);
            na.isStopped = false;
            na.speed = walkingSpeed;
        }

        // If Zombie is in back to base state
        if (asi.IsName("Standing Run Forward"))
        {
            na.SetDestination(target.position);
            na.isStopped = false;
            na.speed = chaseSpeed;
        }
    }

    // Exo is attacking the Player. This is called from the Attack animation of
    // the Exo
    public void Hit()
    {
        if (countdowntimer > 0)
        {
            return;
        }
        countdowntimer = attackSpeed;
        Debug.Log("exo is attacking");
        // tell the attacked game object it has been attacked
        GameManager.Instance.SendMessage("EnemyAttack", null, SendMessageOptions.DontRequireReceiver);
        
    }

    // The player has attacked this Warrior
    // So fire the trigger 
    public void Attacked()
    {
        Debug.Log(name + " has been attacked");

    }
}
