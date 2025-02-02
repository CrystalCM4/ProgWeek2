using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private int randomType;
    

    // Start is called before the first frame update
    void Start()
    {
        hp = 4;
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

        //start moving down
        StartState(EnemyState.Move);

    }

    // Update is called once per frame
    void Update()
    {
        bodyText.text = hp.ToString();
        UpdateState();

        if (transform.position.y <= -6){
            print("game over");
        }
    }

    private void StartState(EnemyState newState){

        //run "end state" of current state to start new state
        EndState(_currentState);

        switch(newState){

            case EnemyState.Move:

                break;

            case EnemyState.Follow: 

                break;

            case EnemyState.Fight: 

                break;
        }
    }

    private void UpdateState(){
        switch(_currentState){

            case EnemyState.Move:

                //move down
                float yPos = transform.position.y;
                yPos -= speed * Time.deltaTime;
                transform.position = new Vector2(transform.position.x, yPos);

                break;

            case EnemyState.Follow: 

                break;

            case EnemyState.Fight: 

                break;

        }
    }

    private void EndState(EnemyState oldState){
        switch(oldState){
            case EnemyState.Move: 
                
                break;

            case EnemyState.Follow:     

                break;

            case EnemyState.Fight: 
                
                break;
        }
    }
}
