using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Resultシーンの文字列を差し込むもの
//ほぼほぼこれで制御。

public class result : MonoBehaviour {
	public GUIText cong;
	public GUIText cleared;
	public GUIText yourscore;
	
	// Use this for initialization
	void Start () {
		
		if (clearflag.get15flag()){				//1-5まできてたら(clearflagが有効のとき)
			cong.text = "congratulation!!";
			cleared.text = "YOU cleared the game!!";
		} else {									//clearflagが無効(途中でゲームオーバー)してたら
			cong.text = "GAME OVER";
		cleared.text = "better luck next time";
		}

		yourscore.text = "YOUR SCORE IS " +scoresaver.getscore();	//どちらにせよスコアを表示
	}
	
	// Update is called once per frame
	void Update () {	//Spaceキー押されたらStartシーンに戻るため
		if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene ("Start");
        }
	}
}
