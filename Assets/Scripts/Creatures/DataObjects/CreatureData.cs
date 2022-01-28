using UnityEngine;

[CreateAssetMenu(menuName = "Data/Creature Data")]
public class CreatureData : ScriptableObject
{
    #region => ===== Trigger Data =====

    [Header("Players Trigger Settings")]
    [SerializeField]
    private float _yellowTriggerDistance;
    public float YellowTriggerDistance => _yellowTriggerDistance;

    [SerializeField]
    private float _purpleTriggerDistance;
    public float PurpleTriggerDistance => _purpleTriggerDistance;

    #endregion

    #region => ===== Player Effect Data =====

    [Header("Player Effect Settings")]
    [SerializeField, Range(0, 0.15f)]
    private float _purpleRepelper;
    public float PurpleRepelper => _purpleRepelper;

    [SerializeField, Range(0, 50)]
    private float _purpleAtkForce;
    public float PurpleAtkForce => _purpleAtkForce;

    [SerializeField]
    private float _atkForceHeight;
    public float AtkForceHeight => _atkForceHeight;

    [SerializeField]
    private int _purpleAtkDuration;
    public int PurpleAtkDuration => _purpleAtkDuration;


    #endregion

}
