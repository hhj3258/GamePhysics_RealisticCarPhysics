using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator1 : MonoBehaviour
{
    NewCarController newCarController;
    public GameObject Map;
    public GameObject Tile;
    static int cnt = 1;

    private void Start()
    {
        Application.targetFrameRate = 60;
        newCarController = FindObjectOfType<NewCarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Instantiate(Tile, new Vector3(-2.2f, 1.8f, 72 * cnt++), Quaternion.identity, Map.transform);
            Invoke("destroyThis", 5f);
        }
    }

    void destroyThis()
    {
        Destroy(Tile);
    }
}
