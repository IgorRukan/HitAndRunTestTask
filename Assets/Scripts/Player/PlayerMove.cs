using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int i;
    private Vector3 direction;
    public float speed;
    public float arrivalThreshold = 0.1f;
    public bool isFinish;
    public AnimationController animationController;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<AnimationController>();
        i = 0;
        animationController.Stay();
    }

    public void StartWalk()
    {
        SetDirection();
        agent.speed = speed;
        animationController.Walk();
    }

    public void StopWalk()
    {
        animationController.Stay();
        agent.speed = 0f;
    }

    public void SetDirection()
    {
        direction = waypoints[i].position;
        i++;
        if (i == waypoints.Length)
        {
            isFinish = true;
            i = 0;
        }
    }

    private void StopToShoot()
    {
        if (isFinish)
        {
            StopWalk();
            GameManager.Instance.FinishLevel();
            isFinish = false;
        }
        else
        {
            animationController.Shoot();
            GetComponent<PlayerShoot>().canShoot = true;
        }
    }

    private void Update()
    {
        agent.SetDestination(direction);
        float distanceToTarget = Vector3.Distance(transform.position, direction);

        if (distanceToTarget < arrivalThreshold)
        {
            StopToShoot();
        }
    }
}