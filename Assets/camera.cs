using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Camera))]
public class camera : MonoBehaviour
{
	public string nowLevel;
    public string nextLevel;
    public GameObject se;
    public AudioSource bgm;
    GameObject player;

    public int goal = 0;
    bool clear = false;

    
    // Use this for initialization
    void Start()
    {
        //ゴールしてないよ!っていうやつ
        goal = 0;
        //今動いてるワールドを検出
        nowLevel = nowworld.getworld();
        //ユニティちゃんを検出(カメラの追従用)
        this.player = GameObject.Find("DemoUnityChan2D");
        //BGMの再生開始。(BGMはInspectorで設定)
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //カメラの位置移動
        //プレイヤーの位置を取得
        Vector3 playerPos = this.player.transform.position;
        //カメラの位置を設定(プレイヤーの座標+6)
        transform.position = new Vector3(-1, playerPos.y + 6f, transform.position.z);
        //普通にやるとユニティちゃんが左右反転でカメラも左右反転で死ぬのでローテーションを固定
		transform.rotation = Quaternion.Euler(0,0,0);

        //もしゴール範囲内に入ったら
		if ((goal == 0) && (playerPos.x <= 0) && (playerPos.y >= 91)){
			StartCoroutine(INTERNAL_Clear());
            //enabled = false;
		}

        // FOR DEBUG: FORCE CLEAR
        if ((goal == 0) && (Input.GetKeyDown(KeyCode.B))) {
            StartCoroutine(INTERNAL_Clear());
        }

        //死んでたらBGM止める(クリア時とは別の挙動)
        if (count.gethit() == 3){
            bgm.Stop();
        }

        
        // ゴールジングル鳴り終わったらシーン遷移
        if ((clear) && (goal == 1) && (!se.GetComponent<playse>().goalplaying())){
            Debug.Log("シーンを変えますよ!");
            changeScene();
        }

        

    }
	private IEnumerator INTERNAL_Clear()
    {
        bgm.Stop();
        se.GetComponent<playse>().playgoal();
        goal = 1;
        Debug.Log("cleared");
        //scoresaver.setscore((int.Parse(scoresaver.getscore()) + (timesaver.gettime()*10)).ToString());
        scoresaver.setscore( ( (Convert.ToInt32(scoresaver.getscore())) + (timesaver.gettime()*10) ) .ToString("0000000") );
        se.GetComponent<playse>().playgoal();
                
        //var player = GameObject.FindGameObjectWithTag("Player");

        //if (player)
        //{
        //    player.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        //}

        yield return new WaitForSeconds(0);
    }

    private void changeScene(){
        
        clear = true;
        if (nowLevel == "1-1"){
            nextLevel = "1-2";
        } else if (nowLevel == "1-2"){
            nextLevel = "1-3";
        } else if (nowLevel == "1-3"){
            nextLevel = "1-4";
        } else if (nowLevel == "1-4"){
            nextLevel = "1-5";
        } else if (nowLevel == "1-5"){
            clearflag.setflag();
            nextLevel = "result";
        }
        nowworld.setworld(nextLevel);
        if (clearflag.getflag() == 1){
            SceneManager.LoadScene (nextLevel);
        } else {
            SceneManager.LoadScene ("Loading " + nextLevel);
        // Application.LoadLevel(nextLevel);
        }
    }
    
}