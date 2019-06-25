using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearflag : MonoBehaviour {

	public static int flag = 0;

	public static int getflag(){
		return flag;
	}

	public static void setflag(){
		flag = 1;
		// 1ならクリアした 0なら死んだ
	}

}
