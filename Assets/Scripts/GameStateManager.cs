using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    //[SerializeField]GameObject CountText;
    [SerializeField] GameObject GoalText;
    [SerializeField]GameObject StartText;
    [SerializeField] GameObject Player;



    //[SerializeField]GameObject TimeText;
    //TextMesh count;
    TextMesh start;
    TextMesh goal;
    // TextMesh time;
    GameObject player;

    int countNum;
    int scoreNum;
    float timeNum;
    int currentState;



    bool ready = false;
    enum STATE
    {
        NONE   = 0,
        READY  = 1,
        PLAY   = 2,
        RESULT = 3
    }

    // Use this for initialization
    void Start () {
        goal = GoalText.GetComponent<TextMesh>();
        start = StartText.GetComponent<TextMesh>();
        


        //time = TimeText.GetComponent<TextMesh>();
        currentState = (int)STATE.NONE;
        start.text = "ボタンを押して\n" +
                     "スタート";

        InitGame();
	}
	
	// Update is called once per frame
	void Update () {

        if (currentState == (int)STATE.NONE)
        {
            if (Input.anyKey)
                StartCoroutine(CountDown());
        }else if (currentState == (int)STATE.READY)
        {
            
        }
        else if(currentState == (int)STATE.PLAY)
        {
            ReduceTime();
            if (timeNum <= 0)
                currentState = (int)STATE.RESULT;
        }
        else if(currentState == (int)STATE.RESULT)
        {
            goal.text = "ゲーム終了\n" +
                         "Rキーで\n" +
                         "もう一度プレイ";

            if (Input.GetKey(KeyCode.R))
            {
                InitGame();
                Player.transform.position = new Vector3(0, 0, 0);//スタート地点に戻る
                StartCoroutine(CountDown());
                
            }

        }
        
	}


    public void InitGame()
    {
        countNum = 3;
        scoreNum = 0;
        timeNum = 60f;
        goal.text = null;
       // time.text  = null;

        
    }
    public void ReduceTime()
    {
        timeNum -= Time.deltaTime;
      //  time.text = timeNum.ToString("f0") + "秒";
    }
    public void SetScore()
    {
        scoreNum ++;
       // score.text = scoreNum.ToString() + "点";

    }

    public int GetState()
    {
        return currentState;
    }

    private IEnumerator CountDown()
    {
        currentState = (int)STATE.READY;
        //score.text = scoreNum.ToString() + "点";
        //time.text = timeNum.ToString("f0") + "秒";
        start.text = "準備はいい？";
        yield return new WaitForSeconds(3f);
        start.text = "はじめ！！" ;
        currentState = (int)STATE.PLAY;
        yield return new WaitForSeconds(1f);
        start.text = null;

    }
}
