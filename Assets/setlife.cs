﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setlife : MonoBehaviour {
	public GUIText life;
	private int a;
	
	// Use this for initialization
	void Start () {
		a = count.getlife();
		life.text = a.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
