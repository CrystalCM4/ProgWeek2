using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private float randomSpawnTime;
    private float randomXPos;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnTime = 0;
        randomXPos = Random.Range(-5,6);
    }

    // Update is called once per frame
    void Update()
    {
        randomSpawnTime -= Time.deltaTime; 
        if (randomSpawnTime <= 0){

            //spawn enemy
            Instantiate(enemy, new Vector3(randomXPos, 6, 0), Quaternion.identity);

            //reset variables
            randomSpawnTime = Random.Range(0,10);
            randomXPos = Random.Range(-5,6);
        }
    }
}
