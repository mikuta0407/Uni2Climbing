using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1-5をクリアしたかしないかを記録するためだけのもの。
//グローバル変数代わり
//もしかしたらタイムオーバー時のが来るかもしれない?
public class clearflag : MonoBehaviour {

	//やってることはそのまんまです。Boolにすればよかったかも・・・
	public static int flag = 0;

	public static int getflag(){
		return flag;
	}

	public static void setflag(){
		flag = 1;
		// 1ならクリアした 0なら死んだ
	}

}
