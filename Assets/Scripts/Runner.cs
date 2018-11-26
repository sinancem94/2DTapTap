using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sadece playeri hareket ettiriyor zamanla hızı artırıyor

public class Runner : MonoBehaviour {
    
    public float speed;
    private float timer;

    void Start () {
        timer = 4f;
        speed = .05f;
        this.transform.position = Platform.instance.transform.GetChild(0).position;
	}
	
	void Update () 
    {
        if(this.transform.position.y <= Platform.instance.platfotmTiles[Platform.instance.blockToSlide].transform.position.y - 1f)// && !Mathf.Approximately(platform.transform.GetChild(platform.GetComponent<Platform>().blockToSlide).position.y,0))
        {
            this.transform.Translate(0f, speed, 0f, Space.World);
        }
        if(Time.unscaledTime > timer)
        {
            speed += .01f;
            timer += 4f;
        }
	}
}
