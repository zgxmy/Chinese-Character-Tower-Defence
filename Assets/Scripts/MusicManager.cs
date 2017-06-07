using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public GameObject music;
    private GameObject _music;
	// Use this for initialization
	void Start () {
        _music = GameObject.FindGameObjectWithTag("bgm");
        if (_music == null) {
            _music = Instantiate(music);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
