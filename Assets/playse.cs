﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SE(ジングル)を再生・それの検知をするもの

public class playse : MonoBehaviour {

    public AudioSource goal;
	public AudioSource die;
	public AudioSource gameover;

	public AudioSource finalclear;

    public void playgoal () {		//ゴールしたとき
        goal.Play();
	}

	public void playdie () {		//死んじゃったとき
        die.Play();
    }

    public void playgameover () {	//死にすぎてゲームオーバーしちゃったとき
       gameover.Play();
    }

	public void playfinal () {		//1-5ゴールしたとき
        finalclear.Play();
	}

	public bool goalplaying(){		//ゴールジングルが鳴ってるか鳴ってないかを通知
		return goal.isPlaying;
	}

	public bool dieplaying(){		//Dieジングルが鳴ってるか鳴ってないかを通知
		return die.isPlaying;
	}

	public bool gameoverplaying(){	//GameOverジングルが鳴ってるか鳴ってないかを通知
		return gameover.isPlaying;
	}

	public bool finalplaying(){		//1-5ジングルが鳴ってるか鳴ってないかを通知
		return finalclear.isPlaying;
	}
	
}