using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class YemOlustur : MonoBehaviour
{
    float RotX;
    float RotY;
    Vector2 spawnalan;
    public float spawnRate;
    public float NextSpawn;

    public List<GameObject> SpawnList;

    int SpawnAdet = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > NextSpawn)
        {
            NextSpawn = Time.time + spawnRate;
            RotX = Random.Range(-49.5f, 49.5f);
            RotY = Random.Range(-49.5f, 49.5f);
            spawnalan=new Vector2(RotX, RotY);

           // Instantiate(SpawnList[Random.Range(SpawnList.Count)], spawnalan, Quaternion.identity);
            
        }

        for(int i = 0; i < SpawnAdet; i++)
        {
            if (i < 50)
            {
                Instantiate(SpawnList[Random.Range(SpawnList.Count,SpawnList.Count)], spawnalan, Quaternion.identity);
            }else if (Time.time > NextSpawn)
            {
                NextSpawn = Time.time + spawnRate;
                RotX = Random.Range(-49.5f, 49.5f);
                RotY = Random.Range(-49.5f, 49.5f);
                spawnalan = new Vector2(RotX, RotY);

                Instantiate(SpawnList[Random.Range(SpawnList.Count, SpawnList.Count)], spawnalan, Quaternion.identity);

            }
        }


    }
}
