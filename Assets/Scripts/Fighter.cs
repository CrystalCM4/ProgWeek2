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

    [SerializeField]
    private Sprite fireSprite;

    [SerializeField]
    private Sprite waterSprite;

    [SerializeField]
    private Sprite grassSprite;


    private Sprite typeSprite;

    private EnemyState _currentState = EnemyState.Move;
    private GameObject target;
    private int hp;
    private int type;
    private bool focused;
    

    // Start is called before the first frame update
    void Start()
    {
        hp = 4;

        type = Manager.nextFightersInt[0];
        typeSprite = GetComponent<SpriteRenderer>().sprite;
        Manager.ChangeFighterType();

        //change sprite
        if (type == 1){

            //fire
            typeSprite = fireSprite;

        }
        else if (type == 2){

            //water
            typeSprite = waterSprite;

        }
        else if (type == 3){

            //grass
            typeSprite = grassSprite;

        }
        gameObject.GetComponent<SpriteRenderer>().sprite = typeSprite;

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

            if (type == 0){
                Manager.normalDeath = true;
            }
            else if (type == 1){
                Manager.fireDeath = true;
            }
            else if (type == 2){
                Manager.waterDeath = true;
            }
            else if (type == 3){
                Manager.grassDeath = true;
            }
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

                if (target != null){
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f);

                    if (transform.position == target.transform.position){
                        StartState(EnemyState.Fight);
                    }
                }
                else {
                    StartState(EnemyState.Move);
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
                    focused = false;
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
            
            if (!focused){

                //prevent from switching targets
                target = collObj.gameObject;
                focused = true;
            }
            
            StartState(EnemyState.Follow);
        }

    }
}
