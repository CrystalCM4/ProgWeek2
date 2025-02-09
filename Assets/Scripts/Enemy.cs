using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{   
    [SerializeField]
    private enum EnemyState {
        Move,
        Follow,
        Fight
    }

    [SerializeField]
    private TextMeshProUGUI bodyText;

    [SerializeField]
    private int speed;

    [SerializeField]
    private Sprite fireSprite;

    [SerializeField]
    private Sprite waterSprite;

    [SerializeField]
    private Sprite grassSprite;


    //private Color typeColor;
    private Sprite typeSprite;
    private EnemyState _currentState = EnemyState.Move;
    private int hp;

    //getter setter
    private int randomType;
    public int RandomType
    {
        get
        {
            return randomType;
        }
        set
        {
            randomType = value;
        }
    }

    private GameObject target;
    

    // Start is called before the first frame update
    void Start()
    {
        hp = 8;
        randomType = Random.Range(0, 4);
        typeSprite = GetComponent<SpriteRenderer>().sprite;

        if (randomType == 1){

            //fire
            typeSprite = fireSprite;

        }
        else if (randomType == 2){

            //water
            typeSprite = waterSprite;

        }
        else if (randomType == 3){

            //grass
            typeSprite = grassSprite;

        }

        //change sprite
        gameObject.GetComponent<SpriteRenderer>().sprite = typeSprite;

    }

    // Update is called once per frame
    void Update()
    {
        bodyText.text = hp.ToString();

        if (hp <= 0){
            Destroy(gameObject);
            Manager.score += 1;
            if (randomType == 0){
                Manager.normalDeath = true;
            }
            else if (randomType == 1){
                Manager.fireDeath = true;
            }
            else if (randomType == 2){
                Manager.waterDeath = true;
            }
            else if (randomType == 3){
                Manager.grassDeath = true;
            }
        }

        if (transform.position.y <= -6){
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

        //move down
            float yPos = transform.position.y;
            yPos -= speed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, yPos);
    }

    public void GetAttacked(int damage){
        hp -= damage;
    }
}
