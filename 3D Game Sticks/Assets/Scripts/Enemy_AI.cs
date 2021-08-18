using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [Tooltip("Tag name of other character(player friend)")]
    public string peopleTag;
    [Tooltip("Gun Reload time of enemy")]
    public float reloadTime;
    [Tooltip("Gun refrence")]
    public Transform gunTransform;
    [Tooltip("How many bullet enemy get in one time")]
    public int bulletAtOnce;
    [Tooltip("Damage this character can give to other character")]
    public int Damage;
    [Tooltip("How much time will there between two fire")]
    public float waitTime;

    private GameObject[] _people;
    private int _ramdompeople;
    private float _timetofire = 0;
    private int _remainingbullet;

    void Start()
    {
        _people = GameObject.FindGameObjectsWithTag(peopleTag);
        _remainingbullet = bulletAtOnce;
        _ramdompeople = Random.Range(0, _people.Length);
        transform.LookAt(_people[_ramdompeople].transform);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));
        gunTransform.LookAt(_people[_ramdompeople].transform.GetChild(2).gameObject.transform);      
    }

    // Update is called once per frame
    void Update()
    {
       shoot();
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
        //Gizmos.DrawLine(gunTransform.position, _people[_ramdompeople].transform.GetChild(2).gameObject.transform.position);
    }

    void shoot()
    {
        if (_people[_ramdompeople] == null)
        {
            _ramdompeople = Random.Range(0, _people.Length);
            if (_people[_ramdompeople] != null)
            {
                transform.LookAt(_people[_ramdompeople].transform);
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));
                gunTransform.LookAt(_people[_ramdompeople].transform.GetChild(2).gameObject.transform);
            }
        }

        if (_people[_ramdompeople] != null)
        {
            gunTransform.LookAt(_people[_ramdompeople].transform.GetChild(2).gameObject.transform);
            if (_remainingbullet <=0)
            {
             Invoke("reload", 2);
            }

           RaycastHit hit;

           if (Physics.Raycast(gunTransform.position, gunTransform.forward, out hit) && _timetofire <= Time.time)
           {
              _remainingbullet--;
              if (hit.transform.gameObject.tag == peopleTag)
              {
                hit.transform.gameObject.GetComponent<Health>().Damage(Damage);
              }
             _timetofire = waitTime + Time.time;
           }
        }
    }

    void reload()
    {
        _remainingbullet = bulletAtOnce;
    }
}
