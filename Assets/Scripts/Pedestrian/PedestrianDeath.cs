using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PedestrianDeath : MonoBehaviour
{
    public static event Action OnPedestrianDeath;

    [SerializeField] int carLayer;
    [SerializeField] float despawnTime;
    Rigidbody rb;
    PedestrianMovement movement;
    NavMeshAgent navAgent;

    bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PedestrianMovement>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == carLayer && !isDead)
        {
            rb.isKinematic = false;
            movement.enabled = false;
            navAgent.enabled = false;
            GameManager.Instance.DeadPedestrian();
            StartCoroutine(Despawn());
            isDead = true;
            OnPedestrianDeath?.Invoke();
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
