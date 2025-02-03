using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private Color typeColor;
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

        if (randomType == 0){

            //normal
            typeColor = Color.white;

        }
        else if (randomType == 1){

            //fire
            typeColor = Color.red;

        }
        else if (randomType == 2){

            //water
            typeColor = Color.blue;

        }
        else if (randomType == 3){

            //grass
            typeColor = Color.green;

        }

        //change color
        gameObject.GetComponent<SpriteRenderer>().color = typeColor;

    }

    // Update is called once per frame
    void Update()
    {
        bodyText.text = hp.ToString();

        if (hp <= 0){
            Destroy(gameObject);
            Manager.score += 1;
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
