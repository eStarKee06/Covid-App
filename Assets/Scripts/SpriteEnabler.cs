using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEnabler : MonoBehaviour
{

    public GameObject food;
    public GameObject soap;
    public GameObject mask;
    public GameObject healthReport;
    
    public GameObject home;
    public GameObject grocery;
    public GameObject work;
    public GameObject hospital;
    
    public GameObject sleep;
    
    public GameObject buyFood;
    public GameObject buyMask;
    public GameObject buySoap;
    
    public GameObject checkIn;
    public GameObject checkOut;
    
    public GameObject goWork;
    
    Collider2D foodI;
    Collider2D soapI;
    Collider2D maskI;
    Collider2D healthReportI;
    
    Collider2D homeI;
    Collider2D groceryI;
    Collider2D workI;
    Collider2D hospitalI;

    
    Collider2D sleepI;

    
    Collider2D buyFoodI;
    Collider2D buyMaskI;
    Collider2D buySoapI;

    
    Collider2D checkInI;
    Collider2D checkOutI;

    
    Collider2D goWorkI;

    public void initialize(){
        this.foodI = this.food.GetComponent<Collider2D>();
        Debug.Log("FOOOOD: " + this.foodI);
        this.maskI = this.mask.GetComponent<Collider2D>();
        this.soapI = this.soap.GetComponent<Collider2D>();
        this.healthReportI = this.healthReport.GetComponent<Collider2D>();
                
        this.homeI = this.home.GetComponent<Collider2D>();
        this.groceryI = this.grocery.GetComponent<Collider2D>();
        this.workI = this.work.GetComponent<Collider2D>();
        this.hospitalI = this.hospital.GetComponent<Collider2D>();
        
        this.sleepI = this.sleep.GetComponent<Collider2D>();

        this.buyFoodI = this.buyFood.GetComponent<Collider2D>();
        this.buyMaskI = this.buyMask.GetComponent<Collider2D>();
        this.buySoapI = this.buySoap.GetComponent<Collider2D>();

        this.checkInI = this.checkIn.GetComponent<Collider2D>();
        this.checkOutI = this.checkOut.GetComponent<Collider2D>();
         
        this.goWorkI = this.goWork.GetComponent<Collider2D>();
    }
    public void enableSprites(){
        this.foodI.enabled = true;
        Debug.Log("FOOOOD2: " + this.foodI);
        this.soapI.enabled = true;
        this.maskI.enabled = true;
        this.healthReportI.enabled = true;
    
        this.homeI.enabled = true;
        this.groceryI.enabled = true;
        this.workI.enabled = true;
        this.hospitalI.enabled = true;
    
        this.sleepI.enabled = true;

        this.buyFoodI.enabled = true;
        this.buyMaskI.enabled = true;
        this.buySoapI.enabled = true;

        this.checkInI.enabled = true;
        this.checkOutI.enabled = true;

        this.goWorkI.enabled = true;
    }
    public void disableSprites(){
        
                Debug.Log("FOOOOD3: " + this.foodI);
        this.foodI.enabled = false;
        this.soapI.enabled = false;
        this.maskI.enabled = false;
        this.healthReportI.enabled = false;
    
        this.homeI.enabled = false;
        this.groceryI.enabled = false;
        this.workI.enabled = false;
        this.hospitalI.enabled = false;
    
        this.sleepI.enabled = false;

        this.buyFoodI.enabled = false;
        this.buyMaskI.enabled = false;
        this.buySoapI.enabled = false;

        this.checkInI.enabled = false;
        this.checkOutI.enabled = false;

        this.goWorkI.enabled = false;
    }
}
