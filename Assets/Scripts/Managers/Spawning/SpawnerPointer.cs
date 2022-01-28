using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPointer : MonoBehaviour
{

    #region => ===== Update =====

    private void OnEnable()
    {
        SpawnManager.ActiveSpawners.Add(this);
    }
    private void OnDisable()
    {
        SpawnManager.ActiveSpawners.Remove(this);
    }

    #endregion


}
