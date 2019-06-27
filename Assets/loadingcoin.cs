using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loading系シーンでCoin数を表示するためだけのもの。
//scoresaver.csからコイン情報を受け取って、GUITextに差し込む。

public class loadingcoin : MonoBehaviour {
	public GUIText coin;
	// Use this for initialization
	void Start () {
		coin.text = scoresaver.getcoin();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
