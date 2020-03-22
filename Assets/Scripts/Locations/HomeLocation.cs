using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLocation : MonoBehaviour, LocationSubject
{
    public GameObject sleep;
    public GameObject shower;
    
    private CircleCollider2D sleepCollider;
    private CircleCollider2D showerCollider;
    private bool sleepTouched;
    private bool showerTouched;

    GameObject player;
    PlayerStatus playerStats;
    // Start is called before the first frame update
    void Start()
    {
        this.sleepCollider = this.sleep.GetComponent<CircleCollider2D>();
        this.showerCollider = this.shower.GetComponent<CircleCollider2D>();

        this.sleepTouched = false;
        this.showerTouched = false;

        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
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
                if(this.sleepCollider == touchedCollider){
                    this.sleepTouched = true;
                    Debug.Log("sleep icon touched");
                }                
                else if(this.showerCollider == touchedCollider){
                    this.showerTouched = true;
                    Debug.Log("shower icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
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
