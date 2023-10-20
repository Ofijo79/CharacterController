using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cubePrefab;

    public int cubeToSpawn;

    //public Transform[] spawnPositions  = new Transform[3]{};

    public Transform[] spawnPositions;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCube", 2f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(cubeToSpawn == 0)
        {
            CancelInvoke();
        }
    }

    void SpawnCube()
    {
        /*Transform selectedSpawn = spawnPositions[Random.Range(0, spawnPositions.Length)];

        Instantiate(goombaPrefab, selectedSpawn.position, selectedSpawn.rotation);*/

        foreach(Transform spawn in spawnPositions)
        {
            Instantiate(cubePrefab, spawn.position, spawn.rotation);
        }

        Transform randomSpawn = spawnPositions[Random.Range(0,4)];

        Instantiate(cubePrefab, randomSpawn.position, randomSpawn.rotation);

        /*for(int i = 0; i < spawnPositions.Length; i++)
        {
            Instantiate(goombaPrefab, spawnPositions[i].position, spawnPositions[i].rotation);
        }*/

        /*int i = 0;
        while(i < spawnPositions.Length)
        {
            Instantiate(goombaPrefab, spawnPositions[i].position, spawnPositions[i].rotation);
            i++;
        }*/

        /*int i = 0;
        do
        {
            Instantiate(goombaPrefab, spawnPositions[i].position, spawnPositions[i].rotation);
            i++;
        }while(i < spawnPositions.Length);*/

        cubeToSpawn--;
    }
}
