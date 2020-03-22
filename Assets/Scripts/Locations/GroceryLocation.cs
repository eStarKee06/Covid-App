using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryLocation : MonoBehaviour, LocationSubject
{
    public GameObject foodChoice; 
    public GameObject soapChoice;
    public GameObject maskChoice;

    private Collider2D foodChoiceCol;
    private Collider2D soapChoiceCol;
    private Collider2D maskChoiceCol;

    private bool foodChoiceTouched;
    private bool soapChoiceTouched;
    private bool maskChoiceTouched;

    GameObject player;
    PlayerStatus playerStats;
    // Start is called before the first frame update
    void Start()
    {
        this.foodChoiceCol = this.foodChoice.GetComponent<Collider2D>();
        this.soapChoiceCol = this.soapChoice.GetComponent<Collider2D>();
        this.maskChoiceCol = this.maskChoice.GetComponent<Collider2D>();
    
        this.foodChoiceTouched = false;
        this.soapChoiceTouched = false;
        this.maskChoiceTouched = false;
    
        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        touchChecker();
    }

    void touchChecker(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.foodChoiceCol == touchedCollider){
                    this.foodChoiceTouched = true;
                    Debug.Log("buy food icon touched");
                }                
                else if(this.soapChoiceCol == touchedCollider){
                    this.soapChoiceTouched = true;
                    Debug.Log("buy food icon touched");
                }              
                else if(this.maskChoiceCol == touchedCollider){
                    this.maskChoiceTouched = true;
                    Debug.Log("buy mask icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.foodChoiceTouched){
                    this.handleBuyFood();
                    this.foodChoiceTouched = false;
                }
                else if(this.soapChoiceTouched){
                    this.handleBuySoap();
                    this.soapChoiceTouched = false;
                }
                else if(this.maskChoiceTouched){
                    this.handleBuyMask();
                    this.maskChoiceTouched = false;
                }
            }
        }
    }

    void handleBuyFood(){
        this.notifyObservers("GROCERY BUY FOOD");
    }
    
    void handleBuySoap(){
        this.notifyObservers("GROCERY BUY SOAP");
    }

    void handleBuyMask(){
        this.notifyObservers("GROCERY BUY MASK");
    }

    public void notifyObservers(string tag){
        this.playerStats.updateFromLocation(tag);
    }
}
