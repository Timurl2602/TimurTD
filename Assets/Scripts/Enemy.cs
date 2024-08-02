using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private Transform _targetWaypoint;
    [SerializeField] private int _waypointIndex = 0;

    public float Speed = 2;

    private void Start()
    {
        _targetWaypoint = _waypoints[_waypointIndex];
    }

    private void Update()
    {
        MoveTowardsWaypoint();
    }
    
    private void MoveTowardsWaypoint()
    {
        Vector3 direction = _targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * (Speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _targetWaypoint.position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        _waypointIndex++;
        if (_waypointIndex >= _waypoints.Count)
        {
            Destroy(gameObject);
            return;
        }
        _targetWaypoint = _waypoints[_waypointIndex];
    }
}
