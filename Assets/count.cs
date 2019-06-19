using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour {

	//敵接触回数
	public static int hit;
	
	public static int gethit(){
		return hit;
	}

	public static void addhit(int a){
		hit = a;
	}
}
