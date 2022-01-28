using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region => ===== Data =====

    [SerializeField]
    private SpawnManagerData _data;
    public SpawnManagerData Data => _data;

    // TODO: use spawn areas instead!
    public static List<SpawnerPointer> ActiveSpawners = new List<SpawnerPointer>();

    [SerializeField]
    private Transform _creaturesContainer;
    public Transform CreaturesContainer => _creaturesContainer;

    [SerializeField]
    private bool _shouldSpawn = true;
    public bool ShouldSpawn => _shouldSpawn;

    #endregion

    #region => ===== MonoBehaviour Methods =====

    private void OnEnable()
    {
        StartCoroutine(runSpawnLoop());
    }

    #endregion

    #region => ===== Spawn Methods =====

    private void spawnCreature(CreatureController creature)
    {
        GameObject creatureGO = Instantiate(creature.gameObject, getSpawnPoint());
        creatureGO.transform.SetParent(CreaturesContainer);
    }

    private CreatureController getCreature(bool isAnimal)
    {
        if (isAnimal)
        {
            return _data.AnimalsToLoad[Random.Range(0, _data.AnimalsToLoad.Length)];
        }
        else
        {
            return _data.MonstersToLoad[Random.Range(0, _data.MonstersToLoad.Length)];
        }
    }

    private Transform getSpawnPoint()
    {
        // TODO: use spawn areas instead!!!
        return ActiveSpawners.Count != 0 ? ActiveSpawners[Random.Range(0, ActiveSpawners.Count)].transform : _creaturesContainer;
    }

    IEnumerator runSpawnLoop()
    {
        yield return new WaitForSeconds(_data.SpawnIntervalTime);
        while (_shouldSpawn)
        {
            for (int i = 0; i < Random.Range(0, 5); i++)
            {
                spawnCreature(getCreature(Random.Range(0, 2) == 1));
            }
            yield return new WaitForSeconds(_data.SpawnIntervalTime);
        }
    }

    #endregion

}
