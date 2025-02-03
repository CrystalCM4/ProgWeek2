//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject fighter;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI nFList;

    [SerializeField]
    private TextMeshProUGUI nFTimer;

    private float randomSpawnTime;
    private float randomXPos;
    private float clickCooldown;
    public static List<string> nextFighters = new();
    public static List<int> nextFightersInt = new();

    public static int score;
    public static int randomFighterType;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnTime = 0;
        randomXPos = Random.Range(-5,6);

        //global variable
        randomFighterType = Random.Range(0,4);

        for (int i = 0; i < 4; i ++){
            
            string next;

            if (randomFighterType == 0){
                next = "Normal";
            }
            else if (randomFighterType == 1){
                next = "Fire";
            }
            else if (randomFighterType == 2){
                next = "Water";
            }
            else {
                next = "Grass";
            }

            nextFighters.Add(next);
            nextFightersInt.Add(randomFighterType);

            randomFighterType = Random.Range(0,4);
        }
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

        //click
        if (Input.GetMouseButtonDown(0) && clickCooldown <= 0){
            
            Instantiate(fighter, point, Quaternion.identity);
            clickCooldown = 2;
        }

        clickCooldown -= Time.deltaTime; 
        if (clickCooldown <= 0){
            clickCooldown = 0;
        }

        scoreText.text = "Score: " + score.ToString();
        if (clickCooldown <= 0){
            nFTimer.text = "Next Fighters";
        }
        else {
            nFTimer.text = "Next Fighters (Cooldown: " + Mathf.Ceil(clickCooldown) + ")";
        }
        
        nFList.text = nextFighters[0] + ", " + nextFighters[1] + ", " + nextFighters[2] + ", " + nextFighters[3];
    }

    public static void ChangeFighterType(){

        nextFighters.RemoveAt(0);
        nextFightersInt.RemoveAt(0);

        string next;

        if (randomFighterType == 0){
            next = "Normal";
        }
        else if (randomFighterType == 1){
            next = "Fire";
        }
        else if (randomFighterType == 2){
            next = "Water";
        }
        else {
            next = "Grass";
        }

        nextFighters.Add(next);
        nextFightersInt.Add(randomFighterType);
        randomFighterType = Random.Range(0,4);
    }
}
