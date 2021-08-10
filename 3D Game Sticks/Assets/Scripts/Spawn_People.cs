using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_People : MonoBehaviour
{
    [Tooltip("People prefap gameobject")]
    public GameObject peoplePrefap;
    [Tooltip("Number of people to spawn")]
    public int peopleToSpawn;
    [Tooltip("Max height of people (keep it higher than stair y-axis value)")]
    public int maxHeightInY = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    void spawn()
    {
        for(int i=0;i< peopleToSpawn; i++)
        {
           Instantiate(peoplePrefap, GetRandomposition(GetComponent<Collider>()) , Quaternion.identity);
        }
    }

    Vector3 GetRandomposition(Collider collider)
    {
        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                                    Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                                    Random.Range(collider.bounds.min.z, collider.bounds.max.z));

        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomposition(collider);
        }

        point.y = maxHeightInY;
        return point;
    }
}
