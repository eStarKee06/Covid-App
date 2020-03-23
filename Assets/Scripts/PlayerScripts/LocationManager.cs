using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    string[] locations = {"HOME", "GROCERY", "WORK", "HOSPITAL"};
    string currentLocation;
    // Start is called before the first frame update
    void Start()
    {
        this.currentLocation = locations[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchLocation(int locationIndex){
        this.currentLocation = locations[locationIndex];
        Debug.Log("switched location: " + this.currentLocation);
    }

    public string getCurrLocation(){
        return this.currentLocation;
    }
}
