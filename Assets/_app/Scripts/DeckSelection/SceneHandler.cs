using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void onBackButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void onPlayGame(){
        DropDownHelper ddh = GameObject.Find("Dropdown Helper").GetComponent<DropDownHelper>();
        DeckSelection.Instance.PlayerDeckChoice = ddh.PlayerDeck.captionText.text.Replace(" ", "");
        DeckSelection.Instance.EnemyDeckChoice = ddh.EnemyDeck.captionText.text.Replace(" ", "");
        SceneManager.LoadScene("GameScene");
    }
}
