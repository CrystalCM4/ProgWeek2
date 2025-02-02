using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fighter : MonoBehaviour
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
    private GameObject target;
    private int hp;
    private int type;
    

    // Start is called before the first frame update
    void Start()
    {
        hp = 4;

        type = GameManager.randomFighterType;

        //change color
        if (type == 0){

            //normal
            typeColor = Color.white;

        }
        else if (type == 1){

            //fire
            typeColor = Color.red;

        }
        else if (type == 2){

            //water
            typeColor = Color.blue;

        }
        else if (type == 3){

            //grass
            typeColor = Color.green;

        }
        gameObject.GetComponent<SpriteRenderer>().color = typeColor;

        //start moving down
        StartState(EnemyState.Move);

    }

    // Update is called once per frame
    void Update()
    {
        bodyText.text = hp.ToString();
        UpdateState();

        if (hp <= 0 || transform.position.y > 6){
            Destroy(gameObject);
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

        _currentState = newState;
    }

    private void UpdateState(){
        switch(_currentState){

            case EnemyState.Move:

                //move down
                float yPos = transform.position.y;
                yPos += speed * Time.deltaTime;
                transform.position = new Vector2(transform.position.x, yPos);

                break;

            case EnemyState.Follow: 

                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f);

                if (transform.position == target.transform.position){
                    StartState(EnemyState.Fight);
                }

                break;

            case EnemyState.Fight: 

                if (target != null){

                    if (type == 0){
                        target.GetComponent<Enemy>().GetAttacked(2);
                        hp -= 2;
                    }
                    else if (type == 1){ //fire
                        
                        //fire vs fire
                        if (target.GetComponent<Enemy>().RandomType == 1 || target.GetComponent<Enemy>().RandomType == 0){
                            target.GetComponent<Enemy>().GetAttacked(2);
                            hp -= 2;
                        }

                        //fire vs water
                        if (target.GetComponent<Enemy>().RandomType == 2){
                            target.GetComponent<Enemy>().GetAttacked(1);
                            hp -= 4;
                        }

                        //fire vs grass
                        if (target.GetComponent<Enemy>().RandomType == 3){
                            target.GetComponent<Enemy>().GetAttacked(4);
                            hp -= 1;
                        }
                    }
                    else if (type == 2){ //water

                        //water vs fire
                        if (target.GetComponent<Enemy>().RandomType == 1){
                            target.GetComponent<Enemy>().GetAttacked(4);
                            hp -= 1;
                        }

                        //water vs water
                        if (target.GetComponent<Enemy>().RandomType == 2 || target.GetComponent<Enemy>().RandomType == 0){
                            target.GetComponent<Enemy>().GetAttacked(2);
                            hp -= 2;
                        }

                        //water vs grass
                        if (target.GetComponent<Enemy>().RandomType == 3){
                            target.GetComponent<Enemy>().GetAttacked(1);
                            hp -= 4;
                        }
                    }
                    else if (type == 3){ //grass
                        
                        //grass vs fire
                        if (target.GetComponent<Enemy>().RandomType == 1){
                            target.GetComponent<Enemy>().GetAttacked(1);
                            hp -= 4;
                        }

                        //grass vs water
                        if (target.GetComponent<Enemy>().RandomType == 2){
                            target.GetComponent<Enemy>().GetAttacked(4);
                            hp -= 1;
                        }

                        //grass vs grass
                        if (target.GetComponent<Enemy>().RandomType == 3 || target.GetComponent<Enemy>().RandomType == 0){
                            target.GetComponent<Enemy>().GetAttacked(2);
                            hp -= 2;
                        }
                    }
                    
                    StartState(EnemyState.Follow);
                }

                else {
                    StartState(EnemyState.Move);
                }

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

    void OnCollisionEnter2D(Collision2D collObj){
        if (collObj.gameObject.CompareTag("Enemy")){
            
            target = collObj.gameObject;
            StartState(EnemyState.Follow);
        }

    }
}
