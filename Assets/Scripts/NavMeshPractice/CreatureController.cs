using System.Collections;
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

    #endregion

    #region => ===== Data =====

    [SerializeField]
    private bool _isPurple;
    public bool IsPurple => _isPurple;

    [SerializeField, Range(0, 0.15f)]
    private float _purpleRepelper;
    public float PurpleRepelper => _purpleRepelper;

    [SerializeField, Range(0.5f,5)]
    private float _purpleAtkForce;
    public float PurpleAtkForce => _purpleAtkForce;

    [SerializeField]
    private int _purpleAtkDuration;
    public int PurpleAtkDuration => _purpleAtkDuration;

    #endregion

    private void Awake()
    {
        _navMeshAgentManager = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Purple") _isPurple = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Purple") _isPurple = false;
    }

    private async Task PurpleAtk()
    {
        float tempRep = _purpleRepelper;
        _purpleRepelper = _purpleAtkForce / _rb.mass; // TODO: This mass is going to have to a different use case.
        await Task.Delay(1000 * PurpleAtkDuration);
        _purpleRepelper = tempRep;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _ = PurpleAtk();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        NavMeshAgentManager.destination = PlayerController.ActivePlayers[(int)Players.Yellow].transform.position;
        if (IsPurple)
        {
            Vector3 v = transform.position - PlayerController.ActivePlayers[(int)Players.Purple].transform.position;
            _navMeshAgentManager.nextPosition += v.normalized * _purpleRepelper;
        }
    }
}
