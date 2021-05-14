using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator1 : MonoBehaviour
{
    [SerializeField] private GameObject maps;

    private int cnt = 1;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(cnt);
            Instantiate(maps, new Vector3(0,0,transform.position.z+100f), Quaternion.identity);
            cnt++;
        }

    }
}
