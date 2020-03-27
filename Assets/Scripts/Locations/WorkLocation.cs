using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkLocation : MonoBehaviour, LocationSubject
{
    CircleCollider2D workCollider;
    bool goToWork;

    public GameObject workButton; 
    Collider2D workButtonCol;

    GameObject player;
    PlayerStatus playerStats;
    LocationManager locManager;
    // Start is called before the first frame update
    void Start()
    {
        this.workCollider = GetComponent<CircleCollider2D>();
        this.player = GameObject.Find("Player");
        this.playerStats =  this.player.GetComponent<PlayerStatus>();
        this.locManager = this.player.GetComponent<LocationManager>();
        this.workButtonCol = this.workButton.GetComponent<Collider2D>();
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
                    this.locManager.switchLocation(2);
                    //Debug.Log("work icon touched");
                }
                if(this.workButtonCol == touchedCollider && this.locManager.getCurrLocation() == "WORK"){
                    this.goToWork = true;
                }
            }

            if(touch.phase == TouchPhase.Ended){
                
                if(this.locManager.getCurrLocation() == "WORK"){
                    if(this.goToWork){
                        //new need to have some sort of timeout later and automatically switch back to home
                        this.notifyObservers("WORK OUTSIDE");
                        this.goToWork = false;
                    }
                }
            }
        }
    }

    public void notifyObservers(string tag){
        this.playerStats.updateFromLocation(tag);
    }
}
