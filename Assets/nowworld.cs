using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nowworld : MonoBehaviour {
	public static string world;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static string getworld(){
		return world;
	}
	public static void setworld(string a){
		world = a;
	}
}
