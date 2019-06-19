using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoresaver : MonoBehaviour {
	public static string score = "0000000";
	public static string highscore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static string getscore(){
		Debug.Log("getscore "+ score);
		return score;
	}
	public static void setscore(string a){
		score = a;
		Debug.Log("setscore " + score);
	}

	public static string gethighscore(){
		return highscore;
	}
	public static void sethighscore(string b){
		highscore = b;
	}
}
