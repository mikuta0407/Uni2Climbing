using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loadingシーンで残基数を表示するためだけのもの。
//count.getlifeで拾ってきてGUITextに投げる
//1-1だったら5を変わりにいれるよ


public class setlife : MonoBehaviour {
	public GUIText life;
	private int a;
	
	// Use this for initialization
	void Start () {
		a = count.getlife();
		life.text = a.ToString();
	}
	
	// Update is called once per frame
	void Update (){
        if (Input.GetKeyDown(KeyCode.F)){
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
