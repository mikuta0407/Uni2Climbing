using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoresaver : MonoBehaviour {
	public static string score = "0000000";
	public static string highscore;

	public static string coin = "00";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static string getscore(){
		//Debug.Log("getscore "+ score);
		return score;
	}
	public static void setscore(string a){
		score = a;
		//Debug.Log("setscore " + score);
	}

	public static string gethighscore(){
		return highscore;
	}
	public static void sethighscore(string b){
		highscore = b;
	}

	public static string getcoin(){
		//Debug.Log("getcoin "+ coin);
		return coin;
	}
	public static void setcoin(string a){
		coin = a;
		//Debug.Log("setcoin " + coin);
	}
}
