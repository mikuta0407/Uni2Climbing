using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1-5をクリアしたかしないかを記録するためだけのもの。
//グローバル変数代わり
//もしかしたらタイムオーバー時のが来るかもしれない?
public class clearflag : MonoBehaviour {

	//やってることはそのまんまです。flagもboolにしたいね
	public static bool flag15 = false; //(1-5)

	public static bool mapclear;

	public static bool timeover = false;

	public static bool gameover = false;

	public static bool fwc = false;

	public static bool died = false;
	public static bool get15flag(){
		return flag15;
	}

	public static void set15flag(bool a){
		flag15 = a;
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

	public static bool isgameover(){
		return gameover;
	}

	public static void truegameover(bool a){
		gameover = a;
	}

	public static bool isfwc(){
		return fwc;
	}

	public static void truefwc(bool a){
		fwc = a;
	}

	public static bool isdied(){
		return died;
	}

	public static void truedied(bool a){
		fwc = died;
	}

}
