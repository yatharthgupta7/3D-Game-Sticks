using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_People : MonoBehaviour
{
    public GameObject peoplePrefap;
    public int peopleToSpawn;

    Collider boxCollider;

    private void Awake()
    {
        if (GetComponent<BoxCollider>() == null)
        {
            this.gameObject.AddComponent<BoxCollider>();
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        boxCollider = GetComponent<BoxCollider>();
        spawnPeople();
        boxCollider.enabled = false;
    }

    void spawnPeople()
    {
        for (int i = 0; i <= 5; i++)
        {
            GameObject people = Instantiate(peoplePrefap);
            people.transform.position = GetRandomPoint();
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 point = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x),
                                    Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y),
                                    Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));

        if (point != boxCollider.ClosestPoint(point))
        {
            point = GetRandomPoint();
        }

        point.y = 6.2f;
        return point;
    }
}
