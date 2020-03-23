using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLocation : MonoBehaviour, LocationSubject
{
    public GameObject sleep;
    public GameObject shower;
    
    private CircleCollider2D homeLocCollider;
    private CircleCollider2D sleepCollider;
    private CircleCollider2D showerCollider;
    private bool sleepTouched;
    private bool showerTouched;

    private bool homeLocTouched;
    



    GameObject player;
    PlayerStatus playerStats;
    LocationManager locManager;
    // Start is called before the first frame update
    void Start()
    {
        this.homeLocCollider = this.GetComponent<CircleCollider2D>();
        this.sleepCollider = this.sleep.GetComponent<CircleCollider2D>();
        this.showerCollider = this.shower.GetComponent<CircleCollider2D>();

        this.sleepTouched = false;
        this.showerTouched = false;
        this.homeLocTouched = false;

        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
        this.locManager = this.player.GetComponent<LocationManager>();
    }

    // Update is called once per frame
    void Update(){
        this.touchCheck();
    }

    void touchCheck(){
            if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.homeLocCollider == touchedCollider){
                    this.homeLocTouched = true;
                }
                if(this.locManager.getCurrLocation() == "HOME"){
                    if(this.sleepCollider == touchedCollider){
                        this.sleepTouched = true;
                        Debug.Log("sleep icon touched");
                    }                
                    else if(this.showerCollider == touchedCollider){
                        this.showerTouched = true;
                        Debug.Log("shower icon touched");
                    }
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.homeLocTouched){
                    this.locManager.switchLocation(0);
                    this.homeLocTouched = false;
                }
                if(this.locManager.getCurrLocation() == "HOME"){
                    if(this.sleepTouched){
                        this.handleSleep();
                        this.sleepTouched = false;
                    }
                    if(this.showerTouched){
                        this.handleShower();
                        this.showerTouched = false;
                    }
                }
            }
        } 
    }

    void handleShower(){
        this.notifyObservers("HOME SHOWER");
    }

    void handleSleep(){
        this.notifyObservers("HOME SLEEP");
    }
    
    public void notifyObservers(string tag){
        this.playerStats.updateFromLocation(tag);
    }
}
