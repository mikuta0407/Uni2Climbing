using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour {

	//敵接触回数
	public static int hit;
	public static int life;
	public static int gethit(){
		return hit;
	}

	public static void sethit(int a){
		hit = a;
	}
	public static int getlife(){
		return life;
	}

	public static void setlife(int a){
		life = a;
		Debug.Log("SetLife : " + life);
	}
}
