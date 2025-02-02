using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject fighter;

    private float randomSpawnTime;
    private float randomXPos;

    public static int randomFighterType;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnTime = 0;
        randomXPos = Random.Range(-5,6);

        //global variable
        randomFighterType = Random.Range(0,4);
    }

    // Update is called once per frame
    void Update()
    {
        randomSpawnTime -= Time.deltaTime; 
        if (randomSpawnTime <= 0){

            //spawn enemy
            Instantiate(enemy, new Vector3(randomXPos, 6, 0), Quaternion.identity);

            //reset variables
            randomSpawnTime = Random.Range(1,10);
            randomXPos = Random.Range(-5,6);
        }

        Vector3 mousePos = Input.mousePosition;
        Camera cam = Camera.main;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        if (Input.GetMouseButtonDown(0)){
            Instantiate(fighter, point, Quaternion.identity);
            randomFighterType = Random.Range(0,4);
        }
    }
}
