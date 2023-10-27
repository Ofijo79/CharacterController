using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;//prefab de enemigo

    [SerializeField]private int enemyToSpawn;//enemigos totales
    private int totalEnemiesToSpawn;//enemigos por cada spawn
    [SerializeField]float waveTime = 60;//tiempo de la oleada
    [SerializeField]int waveNumber = 5;//numero de oleadas

    //public Transform[] spawnPositions  = new Transform[3]{};

    [SerializeField] private Transform[] spawnPositions;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemiesToSpawn = enemyToSpawn / spawnPositions.Length+1;// esto lo hacemos para repartir los enemigos dentro de todos los spawns el uno lo ponemos para que detecte todos los spawns
        StartCoroutine("SpawnEnemy");//empezamos corutina que es una funcion que se puede parar
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyToSpawn <= 0)//si llega a 0 no spawnee enemigos
        {
            StopCoroutine("SpawnEnemy");
        }

        if( waveNumber >= 0)//croonometro que resta tiempo
        {
            waveTime -= Time.deltaTime;
        }
        

        if(waveTime <= 0)// reanudar el tiempo con oleada 
        {
            enemyToSpawn = Random.Range(30, 60);
            totalEnemiesToSpawn = enemyToSpawn / spawnPositions.Length+1;

            StartCoroutine("SpawnEnemy");
            waveTime = 60;
            waveNumber --;
        }
    }

    IEnumerator SpawnEnemy() // Corutina que vamos a usar para Spawnear a enemigos
    {
        Transform randomSpawn = spawnPositions[Random.Range(0, spawnPositions.Length)];//que coja un spawn aleatorio para los enemigos

        for(int i = 0; i < (int)totalEnemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, randomSpawn.position, randomSpawn.rotation);//crea una copia de los prefabs y los pone en un spawn aleatorio
            enemyToSpawn--;
            yield return new WaitForSeconds(1);// para parar corutina y que se espere un segundo
        }    

        StartCoroutine("SpawnEnemy");    
    }
}
