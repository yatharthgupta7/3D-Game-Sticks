using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_People : MonoBehaviour
{
    public GameObject peoplePrefap;
    public int numberOfPeopleToSpawn;
    public float maxHeightInY;


    private void Awake()
    {
        SpawnPeople();
    }
    void SpawnPeople()
    {
        for (int i = 0; i <= 5; i++)
        {
            GameObject people = Instantiate(peoplePrefap);
            people.transform.position = GetRandomPoint(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPoint(Collider collider)
    {
        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                                    Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                                    Random.Range(collider.bounds.min.z, collider.bounds.max.z));

        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPoint(collider);
        }

        point.y = maxHeightInY;
        return point;
    }
}
