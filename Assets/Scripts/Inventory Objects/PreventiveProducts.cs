using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PreventiveProducts : MonoBehaviour, PlayerObserver
{
    List<Preventive> preventiveProducts = new List<Preventive>();
    GameObject player;
    PlayerStatus playerStats;
    Collider2D preventiveCollider;
    bool touchedCol;
    public TMPro.TextMeshProUGUI countText; //implement later
    
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
        this.preventiveCollider = this.GetComponent<Collider2D>();
        this.touchedCol = false;
        this.countText.text = this.preventiveProducts.Count + "x";
    }

    // Update is called once per frame
    void Update()
    {
        this.touchCheck();
    }

    void addPreventiveProduct(){
        this.preventiveProducts.Add(new Preventive());
        
        this.countText.text = this.preventiveProducts.Count + "x";
    }

    void usePreventiveProduct(){
        //check later if empty
        //double returnedVal = this.preventiveProducts[this.preventiveProducts.Count - 1].;
        this.preventiveProducts.RemoveAt(this.preventiveProducts.Count - 1);
    
        this.countText.text = this.preventiveProducts.Count + "x";
    }

    public void notifyObservers(){
        this.usePreventiveProduct();
        /*double newImmuneSys = ((this.playerStats.getImmuneSys() + 0.25) > 1.0) ? 1.25 : (this.playerStats.getImmuneSys() + 0.25);
        (this.playerStats).update("IMMUNE_SYS", newImmuneSys);*/
        (this.playerStats).update("WEAR_MASK", 0);
    }

    void touchCheck(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.preventiveCollider == touchedCollider){
                    this.touchedCol = true;
                   // Debug.Log("food icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.touchedCol){
                    this.notifyObservers();
                    this.touchedCol = false;
                }
            }
        }
    }

    //------------------TESTS--------------------------------
    void testInitial(){
        for(int i = 0; i < 5; i++){
            this.addPreventiveProduct();
        }
    }

    //listener
    public void updateFromPlayer(string tag){
        switch(tag){
            case "GROCERY BUY MASK":
                    this.addPreventiveProduct();
                    break;
        }
       // Debug.Log(this.preventiveProducts.Count);
    }
}
