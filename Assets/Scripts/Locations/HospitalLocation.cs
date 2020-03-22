using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalLocation : MonoBehaviour, LocationSubject
{
    public GameObject checkInChoice;
    public GameObject checkUpChoice;
    
    private CircleCollider2D checkInCollider;
    private CircleCollider2D checkUpCollider;
    private bool checkInTouched;
    private bool checkUpTouched;
    
    GameObject player;
    PlayerStatus playerStats;
    // Start is called before the first frame update
    void Start()
    {
        this.checkInCollider = this.checkInChoice.GetComponent<CircleCollider2D>();
        this.checkUpCollider = this.checkUpChoice.GetComponent<CircleCollider2D>();
        this.checkInTouched = false;
        this.checkUpTouched = false;

        
        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        this.touchChecker();
    }

    void touchChecker(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.checkInCollider == touchedCollider){
                    this.checkInTouched = true;
                    Debug.Log("check in icon touched");
                }                
                else if(this.checkUpCollider == touchedCollider){
                    this.checkUpTouched = true;
                    Debug.Log("check up icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.checkUpTouched){
                    this.handleCheckUp();
                    this.checkUpTouched = false;
                }
                if(this.checkInTouched){
                    this.handleCheckIn();
                    this.checkInTouched = false;
                }
            }
        }
    }

    void handleCheckUp(){
        // give user the report
        notifyObservers("HOSPITAL CHECK UP");
        

    }
    void handleCheckIn(){
        notifyObservers("HOSPITAL CHECK IN");
    }

    public void notifyObservers(string tag){
        this.playerStats.updateFromLocation(tag);
    }

}
