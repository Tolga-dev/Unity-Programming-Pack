using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    private Stack<GameObject> objectsPool = new Stack<GameObject>();

    private void Start()
    {
        StartCoroutine("CreateAndDestroyObject");
    }

    IEnumerator CreateAndDestroyObject()
    {
        while (true)
        {
            GameObject obj = FindObjectFromPool();
            yield return new WaitForSeconds(1f);
            AddObjectsToPool(obj);
            yield return new WaitForSeconds(1f);
        }
    }

    GameObject FindObjectFromPool()
    {
        if (objectsPool.Count > 0)
        {
            GameObject rGameObject = objectsPool.Pop();
            rGameObject.SetActive(true);
            return rGameObject;
        }
        return Instantiate(objectPrefab);
    }

    void AddObjectsToPool(GameObject g)
    {
        g.SetActive(false);
        objectsPool.Push(g);
    }
}
