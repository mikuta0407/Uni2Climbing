﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loading系シーンでPointを表示するためだけのもの。
//scoresaver.csからスコア情報を受け取って、GUITextに差し込む。

public class loadingpoint : MonoBehaviour {
	public GUIText total;
	
	// Use this for initialization
	void Start () {
		total.text = scoresaver.getscore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
