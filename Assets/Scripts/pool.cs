using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pool : MonoBehaviour
{
    public int poolSize = 50;
    GameObject[] objPool;
    public GameObject template;


    public virtual void Awake()
    {
        objPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            objPool[i] = Instantiate(template, transform);
            objPool[i].SetActive(false);
        }
    }

    public GameObject Pull(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject o in objPool)
        {
            if (!o.activeSelf)
            {
                o.SetActive(true);
                o.transform.position = position;
                o.transform.rotation = rotation;
                return o;
            }
        }
        Debug.LogWarning("Pool \"" + gameObject.name + "\" is empty");
        return null;
    }

    public GameObject Timeout(GameObject obj, float time, bool value = false)
    {
        StartCoroutine(setActiveDelay(obj, time, value));
        return obj;
    }

    IEnumerator setActiveDelay(GameObject obj, float time, bool value)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(value);
    }



}
