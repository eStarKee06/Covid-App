
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject instructionsBG;
    public Button closeIns;
    public Button openIns;

    /*void Start(){
        this.instructionsBG.gameObject.SetActive(true);
        this.closeIns.onClick.AddListener(closeInstructions);
        this.openIns.onClick.AddListener(openInstructions);
    }

    void closeInstructions(){
        this.instructionsBG.gameObject.SetActive(false);
    }    
    void openInstructions(){
        this.instructionsBG.gameObject.SetActive(true);
    }*/

    public void resetGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
