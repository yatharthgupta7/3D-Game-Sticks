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
    [Tooltip("Fire Point of gun")]
    public Transform firePointTransform;
    [Tooltip("How many bullet enemy get in one time")]
    public int bulletAtOnce;
    [Tooltip("Damage this character can give to other character")]
    public int Damage;
    [Tooltip("How much time will there between two fire")]
    public float waitTime;
    [Tooltip("People Layer Mask")]
    public LayerMask peopleLayerMask;

    private GameObject[] _people;
    private int _ramdompeople;
    private float _timetofire = 0;
    private int _remainingbullet;
    private Vector3 dir;

    void Start()
    {
        lookAt();
    }

    // Update is called once per frame
    void Update()
    { 
        if (_people.Length>0)
            shoot();
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
                gunTransform.LookAt(_people[_ramdompeople].transform);
            }
        }

        if (_people[_ramdompeople] != null)
        {
            gunTransform.LookAt(_people[_ramdompeople].transform);
           if(_remainingbullet <=0)
           {
             Invoke("reload", reloadTime);
           }

           RaycastHit hit;
           //dir = (_people[_ramdompeople].transform.position - firePointTransform.transform.position).normalized;
            
           if (Physics.Raycast(firePointTransform.position, firePointTransform.forward, out hit , peopleLayerMask) && _timetofire <= Time.time)
           {
              _remainingbullet--;
              Debug.DrawRay(firePointTransform.position, firePointTransform.forward * 1000, Color.red);
              if (hit.transform.gameObject.tag != null)
              {
                 if(hit.transform.gameObject.tag == peopleTag)
                    hit.transform.gameObject.GetComponent<Health>().Damage(Damage);
              }
             _timetofire = waitTime + Time.time;
           }
        }
    }

    void lookAt()
    {
        _people = GameObject.FindGameObjectsWithTag(peopleTag);
        _remainingbullet = bulletAtOnce;
        if (_people.Length > 0)
        {
            _ramdompeople = Random.Range(0, _people.Length);
            transform.LookAt(_people[_ramdompeople].transform);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));
            gunTransform.LookAt(_people[_ramdompeople].transform);
        }
    }

    void reload()
    {
        _remainingbullet = bulletAtOnce;
    }
}
