using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacther : MonoBehaviour
{
    public GameObject[] charactherPrefabs;
    public Transform spawnPoint;

    public void Start()
    {
        int SelectCharacther = PlayerPrefs.GetInt("SelectCharacther");
        GameObject prefab = charactherPrefabs[SelectCharacther];
        prefab.SetActive(true);
    }
}
