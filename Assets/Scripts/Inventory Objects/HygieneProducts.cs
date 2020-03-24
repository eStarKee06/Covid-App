using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HygieneProducts : MonoBehaviour, InventorySubject, PlayerObserver
{
    List<Hygiene> hygieneProducts = new List<Hygiene>();
    GameObject player;
    PlayerStatus playerStats;

    Collider2D hygieneObj;
    
    public TMPro.TextMeshProUGUI countText;

    bool touchHygieneIcon;
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
        this.hygieneObj = GetComponent<Collider2D>();
        this.touchHygieneIcon = false;
        this.countText.text = this.hygieneProducts.Count + "x";
        this.testInitial();    
    }

    // Update is called once per frame
    void Update()
    {
        touchCheck();
    }

    void addHygieneProduct(){
        this.hygieneProducts.Add(new Hygiene());
        this.countText.text = this.hygieneProducts.Count + "x";
    }

    double useHygieneProduct(){
        //check later if empty
        double returnedVal = this.hygieneProducts[this.hygieneProducts.Count - 1].IMMUNE_IMPACT;
        this.hygieneProducts.RemoveAt(this.hygieneProducts.Count - 1);
        this.countText.text = this.hygieneProducts.Count + "x";
        return returnedVal;
    }

    public void notifyObservers(){
        Debug.Log("hygiene notify");
        double currImmuneLvl = this.playerStats.getImmuneSys();
        double incImmuneBy = this.useHygieneProduct();
        double newImmuneLvl = ((currImmuneLvl + incImmuneBy) > 1) ? 1.25 : (currImmuneLvl + incImmuneBy);
        (this.playerStats).update("IMMUNE_SYS", newImmuneLvl);
        Debug.Log("hygiene notify end");
    }

    void touchCheck(){

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.hygieneObj == touchedCollider){
                    this.touchHygieneIcon = true;
                    Debug.Log("hygiene icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.touchHygieneIcon){
                    this.notifyObservers();
                    this.touchHygieneIcon = false;
                }
            }
        }
    }

    //------------------TESTS--------------------------------
    void testInitial(){
        for(int i = 0; i < 5; i++){
            this.addHygieneProduct();
        }
    }

    //listener
    public void updateFromPlayer(string tag){
        switch(tag){
            case "GROCERY BUY SOAP":
                    this.addHygieneProduct();
                    break;
        }
        
        Debug.Log("soap count " + this.hygieneProducts.Count);
    }
}
