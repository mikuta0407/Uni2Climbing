﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Camera))]
public class camera : MonoBehaviour{
	public string nowLevel;     //今のワールド情報を維持
    public string nextLevel;    //次開くワールド情報を格納
    public GameObject se;       //SE再生用
    public AudioSource bgm;     //BGM再生用
    GameObject player;          //プレイヤーを追従するため用

    public static bool goal = false;    //ゴール自体をしたかの判定

    bool clear = false;                 //シーンチェンジに突入したか判定

    bool playedgoal = false;              //ゴールジングルの再生
    bool playedfinal = false;             //1-5ジングルの再生

    bool playedalljingle = false;

    public bool isfc;

    // Use this for initialization
    void Start(){
        //ゴールしてないよ!っていうやつ
        goal = false;
        //今動いてるワールドを検出
        nowLevel = nowworld.getworld();
        //ユニティちゃんを検出(カメラの追従用)
        this.player = GameObject.Find("DemoUnityChan2D");
        //BGMの再生開始。(BGMはInspectorで設定)
        bgm.Play();

        clearflag.set15flag(false);
        clearflag.truemapclear(false);
        clearflag.truetimeover(false);
        clearflag.truegameover(false);
        clearflag.truefwc(false);
        clearflag.truedied(false);

        //Screen.fullScreen = false;
        //Debug.Log("現在のスクリーン状態:↓");
        //Debug.Log(Screen.fullScreen);
    }

    // Update is called once per frame
    void Update(){

        isfc = Screen.fullScreen;
        //カメラの位置移動
        //プレイヤーの位置を取得
        Vector3 playerPos = this.player.transform.position;
        //カメラの位置を設定(プレイヤーの座標+6)
        transform.position = new Vector3(-1, playerPos.y + 6f, transform.position.z);
        //普通にやるとユニティちゃんが左右反転でカメラも左右反転で死ぬのでローテーションを固定
		transform.rotation = Quaternion.Euler(0,0,0);

        //もしゴール範囲内に入ったら
		if ((!clear) && (!goal) && (playerPos.x <= -4) && (playerPos.y >= 91)){
			StartCoroutine(INTERNAL_Clear());
            //enabled = false;
		}

        // FOR DEBUG: FORCE CLEAR
        if ((!clear) && (!goal) && (Input.GetKeyDown(KeyCode.B))) {
            StartCoroutine(INTERNAL_Clear());
        }

        //死んでたらBGM止める(クリア時とは別の挙動)
        if (count.gethit() == 3){
            bgm.Stop();
        }

        
        // ゴールジングル鳴り終わったらシーン遷移
        if ((!clear) && (goal)){
            if (!playedalljingle){
                playjingle();
            } else {
                Debug.Log("シーンを変えますよ!");
                changeScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene ("Start");
        }

        //if (Input.GetKeyDown(KeyCode.F)){
        //    if (Screen.fullScreen == false){
        //        Debug.Log("現在のモードはウィンドウモードです。フルスクリーンに切り替えます。");
        //        Screen.fullScreen = true;
        //        Debug.Log(Screen.fullScreen);
        //        Debug.Log("フルスクリーンに切り替えました");
        //    } else {
        //        Debug.Log("現在のモードはフルスクリーンです。ウィンドウモードに切り替えます。");
        //        Screen.fullScreen = false;
        //        Debug.Log(Screen.fullScreen);
        //        Debug.Log("ウィンドウモードに切り替えました。");
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.P)){
            scoresaver.setcoin("98");
        }
        

    }
	private IEnumerator INTERNAL_Clear(){   //クリアしたら呼び出される
        
        //BGMを停止
        bgm.Stop();

        //1-5は最終ステージなので、オールクリアしたことをclearflag.cs内に記録
        if (nowLevel == "1-5"){
            clearflag.set15flag(true);
        }

        ////ゴールジングルを再生(playse.csにて制御。PLAYSEオブジェクトを使用)
        //se.GetComponent<playse>().playgoal();

        //goalフラッグをtrueに。これしないとUpdateでこれが呼び出されまくる
        goal = true;
        clearflag.truemapclear(true);

        //クリアしたよって一応しゃべる
        Debug.Log("cleared");

        //残り時間をスコアに変換。
        //timesaver.csから時間が取れるので、それをscoresaverを使って記録。
        if (!clearflag.isdied()){
            scoresaver.setscore( ( (Convert.ToInt32(scoresaver.getscore())) + (timesaver.gettime()*10) ) .ToString("0000000") );
        }

        if (clearflag.isdied()){
            scoresaver.setscore( ( (Convert.ToInt32(scoresaver.getscore())) - 600 ) .ToString("0000000") );
        }

        //コルーチンなのでreturnがいるので虚無する。        
        yield return new WaitForSeconds(0);
    }

    private void changeScene(){     //シーンチェンジをします
        
        //シーンチェンジ始まったことを判定
        //これしないと例によってUpdateで永遠に呼び出されるので・・・
        clear = true;

        //今が1-1だったら1-2を・・・みたいな感じ
        if (nowLevel == "1-1"){
            nextLevel = "1-2";
        } else if (nowLevel == "1-2"){
            nextLevel = "1-3";
        } else if (nowLevel == "1-3"){
            nextLevel = "1-4";
        } else if (nowLevel == "1-4"){
            nextLevel = "1-5";
        } else if (nowLevel == "1-5"){
            nextLevel = "Result";
        }

        //現在のレベル状況をこの時点で書き換え。
        nowworld.setworld(nextLevel);
        if (clearflag.get15flag()){
            SceneManager.LoadScene (nextLevel);     //ResultにはLoadingって文字はないので・・・
        } else {
            SceneManager.LoadScene ("Loading " + nextLevel);
        }
    }

    void playjingle(){
        if (!playedgoal){
            se.GetComponent<playse>().playgoal();
            playedgoal = true;
        }
            
        if ((nowLevel == "1-5") &&  ((!se.GetComponent<playse>().goalplaying())) && (clearflag.get15flag()) && (playedgoal) && (!playedfinal)){
            clearflag.truefwc(true);
            se.GetComponent<playse>().playfinal();
            playedfinal = true;
        }

        //if ((!se.GetComponent<playse>().goalplaying()) && (!se.GetComponent<playse>().finalplaying()) && (playedgoal) && ((nowLevel == "1-5") && (playedfinal))){
        if (!se.GetComponent<playse>().goalplaying()) {
            if (!se.GetComponent<playse>().finalplaying()) {
                if (playedgoal) {
                    if ((nowLevel == "1-5") && (playedfinal) || (nowLevel != "1-5")){
                        Debug.Log("必要なジングルが鳴り終わりました");
                        playedalljingle = true;
                    }
                }
            }

        }

    }
    
}