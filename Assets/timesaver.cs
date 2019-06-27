using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間を記録したりするやつ(記録するだけ)
//グローバル変数代わり

public class timesaver : MonoBehaviour {
	public static int time;
	// Use this for initialization
	public static int gettime(){
		//Debug.Log("gettime "+ time);
		return time;
	}
	public static void settime(int a){
		time = a;
		//Debug.Log("setcoin " + time);
	}
}
