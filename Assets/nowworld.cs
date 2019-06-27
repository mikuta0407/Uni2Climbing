using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//今いるステージ位置を記録・取得するもの
public class nowworld : MonoBehaviour {
	public static string world;

	public static string getworld(){
		return world;
	}
	public static void setworld(string a){
		world = a;
	}
}
