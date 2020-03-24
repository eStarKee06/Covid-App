using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour
{
    public GameObject lidPanel;
    public Button locChoiceControl;

    private Animator lidAnimation;
    // Start is called before the first frame update
    void Start()
    {
        this.lidAnimation = this.lidPanel.GetComponent<Animator>();

        this.locChoiceControl.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick(){
        if(lidAnimation != null){
            bool isOpen = lidAnimation.GetBool("openPanel");
            lidAnimation.SetBool("openPanel", !isOpen);
        }
    }
}
