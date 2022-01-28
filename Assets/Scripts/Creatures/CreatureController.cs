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
        NavMeshAgentManager.destination = PlayerController.ActivePlayers[(int)Players.Yellow].transform.position;
        if (_isPurpleInRange)
        {
            Vector3 v = transform.position - PlayerController.ActivePlayers[(int)Players.Purple].transform.position;
            _navMeshAgentManager.nextPosition += v.normalized * _data.PurpleRepelper;
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
                if(_isYellowInRange) _ = YellowAtkTriggered();
                break;
            case Players.Purple:
                if (_isPurpleInRange) _ = PurpleAtkTriggered();
                break;
            default:
                break;
        }
    }

    private async Task PurpleAtkTriggered()
    {
        // 1. disable rigidbody kinematics & nav mesh agent
        toggleNavMeshRigidBody(false);
        // 2. apply explosive force to rigidboy.
        _rb.AddExplosionForce(_data.PurpleAtkForce, PlayerController.ActivePlayers[(int)Players.Purple].transform.position, _data.AtkForceHeight);
        // 3. wait for duration
        await Task.Delay(_data.PurpleAtkDuration * 1000);
        toggleNavMeshRigidBody(true);
    }


    private async Task YellowAtkTriggered()
    {
    }



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
