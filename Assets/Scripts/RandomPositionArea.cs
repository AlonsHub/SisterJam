using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionArea : MonoBehaviour
{
    Collider col;
    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    public Vector3 GetRandomPositionInArea() //
    {
        float x = Random.Range(col.bounds.min.x, col.bounds.max.x);
        float y = Random.Range(col.bounds.min.y, col.bounds.max.y);
        float z = Random.Range(col.bounds.min.z, col.bounds.max.z);

        return new Vector3(x, y, z);
        //col.bounds.

    }
}
