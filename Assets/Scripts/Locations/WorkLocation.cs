using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkLocation : MonoBehaviour, LocationSubject
{
    CircleCollider2D workCollider;
    bool goToWork;

    GameObject player;
    PlayerStatus playerStats;
    // Start is called before the first frame update
    void Start()
    {
        this.workCollider = GetComponent<CircleCollider2D>();
        this.player = GameObject.Find("Player");
        this.playerStats =  this.player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        this.touchCheck();
    }

    void touchCheck(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.workCollider == touchedCollider){
                    this.goToWork = true;
                    Debug.Log("work icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.goToWork){
                    this.notifyObservers("WORK OUTSIDE");
                    this.goToWork = false;
                }
            }
        }
    }

    public void notifyObservers(string tag){
        this.playerStats.updateFromLocation(tag);
    }
}
