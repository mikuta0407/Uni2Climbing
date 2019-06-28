using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1-5をクリアしたかしないかを記録するためだけのもの。
//グローバル変数代わり
//もしかしたらタイムオーバー時のが来るかもしれない?
public class clearflag : MonoBehaviour {

	//やってることはそのまんまです。flagもboolにしたいね
	public static int flag = 0; //(1-5)

	public static bool mapclear;

	public static bool timeover = false;
	public static int getflag(){
		return flag;
	}

	public static void setflag(){
		flag = 1;
		// 1ならクリアした 0なら死んだ
	}

	public static bool istimeover(){
		return timeover;
	}

	public static void truetimeover(bool a){
		timeover = a;
	}

	public static bool ismapclear(){
		return mapclear;
	}

	public static void truemapclear(bool a){
		mapclear = a;
	}

}
