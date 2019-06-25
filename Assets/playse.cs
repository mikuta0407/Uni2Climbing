﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playse : MonoBehaviour {

    public AudioSource goal;
	public AudioSource die;
	public AudioSource gameover;
    public void playgoal () {
        goal = GetComponent<AudioSource>();
        goal.Play();
	}

	public void playdie () {
        die = GetComponent<AudioSource>();
        die.Play();
    }

    public void playgameover () {
        gameover = GetComponent<AudioSource>();
        gameover.Play();
    }

	public bool goalplaying(){
		return goal.isPlaying;
	}

	public bool dieplaying(){
		return die.isPlaying;
	}

	public bool gameoverplaying(){
		return gameover.isPlaying;
	}
}