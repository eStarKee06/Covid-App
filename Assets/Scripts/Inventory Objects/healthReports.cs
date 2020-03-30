using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthReports : MonoBehaviour, PlayerObserver
{
    
    private int count;
    public TMPro.TextMeshProUGUI countText;
    public TMPro.TextMeshProUGUI healthReportText;
    public Button healthReportCloseButton;
    public GameObject mainPanel;

    bool touchedReportIcon;
    bool openReport;

    PlayerStatus playerStats;
    Collider2D reportCollider;

    SpriteEnabler spriteEnabler;

    // Start is called before the first frame update
    void Start()
    {
        this.count = 0;
        this.playerStats = GameObject.Find("Player").GetComponent<PlayerStatus>();
        this.touchedReportIcon = false;
        this.openReport = false;
        this.reportCollider = this.GetComponent<Collider2D>();

        this.countText.text = this.count + "x";
        mainPanel.gameObject.SetActive(false);
        
        //this.spriteEnabler = GameObject.FindObjectOfType<SpriteEnabler>();
        //this.spriteEnabler.disableSprites();
        this.spriteEnabler = GameObject.Find("SpriteEnabler").GetComponent<SpriteEnabler>();
        this.spriteEnabler.initialize();
    }

    void Update(){
            touchCheck();
    }

    void addHealthReport(){
        
        this.count++;
    
        this.countText.text = this.count + "x";
    }
    void useHealthReport(){
        if(this.canUseReport()){
            this.count--;
        }
        
        this.countText.text = this.count + "x";
    }
    bool canUseReport(){
        if(this.count > 0){
            return true;
        }
        return false;
    }

    void touchCheck(){

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.reportCollider == touchedCollider){
                    this.touchedReportIcon = true;
                    Debug.Log("health report icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.touchedReportIcon){
                    this.openReportFunction();
                    this.touchedReportIcon = false;
                }
            }
        }
    }

    void openReportFunction(){
            //this.spriteEnabler.disableSprites();
            this.spriteEnabler.disableSprites();
            if(this.canUseReport()){
                this.openReport = true;
                useHealthReport();
                this.healthReportText.text = this.playerStats.getHealthReport();

                this.mainPanel.gameObject.SetActive(true);
                this.healthReportCloseButton.onClick.AddListener(closeReport);
            }
    }

    void closeReport(){
        this.openReport = false;
        this.mainPanel.gameObject.SetActive(false);
        
            this.spriteEnabler.enableSprites();
        //this.spriteEnabler.disableSprites();
        
        //this.spriteEnabler.enableSprites();
    }

    public void updateFromPlayer(string tag){
        if(tag == "HOSPITAL CHECK UP"){
            this.addHealthReport();

        }
    }
}
