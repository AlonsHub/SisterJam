﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreatureController : MonoBehaviour
{

    #region => ===== Components =====

    [SerializeField]
    private NavMeshAgent _navMeshAgentManager;
    public NavMeshAgent NavMeshAgentManager => _navMeshAgentManager;

    [SerializeField]
    private Rigidbody _rb;
    public Rigidbody RB => _rb;

    [SerializeField]
    private CreatureData _data;
    public CreatureData Data => _data;

    #endregion

    #region => ===== Data =====

    [SerializeField]
    private bool _isPurpleInRange;
    public bool IsPurpleInRange => _isPurpleInRange;

    [SerializeField]
    private bool _isYellowInRange;
    public bool IsYellowInRange => _isYellowInRange;

    #endregion

    private void OnEnable()
    {
        PlayerController.AtkActionEvent += (Players playerType) => { runPlayerAtkSequence(playerType); };
    }

    private void OnDisable()
    {
        PlayerController.AtkActionEvent -= (Players playerType) => { runPlayerAtkSequence(playerType); };
        StopAllCoroutines();
    }

    private void Update()
    {
        CheckIfPlayersInRange();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_navMeshAgentManager.enabled == true) runNavMeshAgent();
    }

    #region => ===== AI Methods =====

    private void runNavMeshAgent()
    {
        if (_isYellowInRange)
            _navMeshAgentManager.destination = PlayerController.ActivePlayers[(int)Players.Yellow].transform.position; //WANDER
        else
            Wander();
            //_navMeshAgentManager.destination = TempCenter.Instance.transform.position; //WANDER
        //_navMeshAgentManager.desiredVelocity = 

        if (_isPurpleInRange)
        {
            Vector3 v = transform.position - PlayerController.ActivePlayers[(int)Players.Purple].transform.position;
            _navMeshAgentManager.nextPosition += v.normalized * _data.PurpleRepelper;
        }
    }
    // Wander attempt 1#
    private void Wander()
    {
        if (_navMeshAgentManager.remainingDistance > _navMeshAgentManager.stoppingDistance + .1f)
            return;

        if (NavMesh.SamplePosition(Vector3.zero + Random.insideUnitSphere * Random.Range(50, 100), out NavMeshHit hit, 120, NavMesh.AllAreas))
        {
            _navMeshAgentManager.destination = hit.position;
        }
        else
        {
            _navMeshAgentManager.destination = Vector3.zero;
        }
    }

    private void toggleNavMeshRigidBody(bool isEnabled)
    {
        _rb.isKinematic = isEnabled;
        _navMeshAgentManager.enabled = isEnabled;
    }

    #endregion


    #region => ===== Player Effects Methods =====
   
   

    private void runPlayerAtkSequence(Players player)
    {
        switch (player)
        {
            case Players.Yellow:
                //if(_isYellowInRange) _ = YellowAtkTriggered();
                break;
            case Players.Purple:
                if (_isPurpleInRange)
                    StartCoroutine(nameof(PurpleAtkCorou));
                break;
            default:
                break;
        }
    }
    IEnumerator PurpleAtkCorou()
    {
        // 1. disable rigidbody kinematics & nav mesh agent
        toggleNavMeshRigidBody(false);
        // 2. apply explosive force to rigidboy.
        yield return new WaitForSeconds(.01f);
        _rb.AddExplosionForce(_data.PurpleAtkForce, PlayerController.ActivePlayers[(int)Players.Purple].transform.position, _data.AtkExplosionRadius, _data.AtkForceHeight, ForceMode.Impulse);
        yield return new WaitForSeconds(_data.PurpleAtkDuration);
        // 3. wait for duration
        toggleNavMeshRigidBody(true);
    }

    //IEnumerator YellowAtkCorou()
    //{
    //    //increase pull of both animal and monsters (pulling the animals MORE than monsters, enough to make animals FASTER than monsters in approaching yellow
    //    while ()
    //    {

    //    }
    //}
    
    #endregion

    #region => ===== Player Distance Methods =====

    public void CheckIfPlayersInRange()
    {
        if(PlayerController.ActivePlayers[(int)Players.Purple] != null)
            _isPurpleInRange = checkIfPurpleInRange();
        if(PlayerController.ActivePlayers[(int)Players.Yellow] != null)
        _isYellowInRange = checkIfYellowInRange();
    }

    private bool checkIfYellowInRange()
    {
        return getDistanceFromPlayer(Players.Yellow) < _data.YellowTriggerDistance;
    }

    private bool checkIfPurpleInRange()
    {
        return getDistanceFromPlayer(Players.Purple) < _data.PurpleTriggerDistance;
    }

    private float getDistanceFromPlayer(Players player)
    {
        return Vector3.Distance(transform.position, PlayerController.ActivePlayers[(int)player].transform.position);
    }

    #endregion
}
