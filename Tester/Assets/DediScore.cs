using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DediScore : MonoBehaviour {

    // Use this for initialization
    public GameObject PlayerScoreBoardItem;
    public Transform PlayerScoreBoardList;

    private void Start()
    {
        Debug.Log("Good");
    }

    private void OnEnable()
    {
        var players = TeamManager.returnPlayers();
        Debug.Log(players);
        foreach (var player in players)
        {
            Color TeamColor = player.GetComponent<setup>().TeamColor;
            GameObject item = (GameObject)Instantiate(PlayerScoreBoardItem, PlayerScoreBoardList);
            if (item != null)
            {
                if (player.isalive == false)
                {
                    item.GetComponent<CanvasGroup>().alpha = 0.2f;
                }
                item.GetComponent<PlayerScoreBoardItem>().setup(player.isalive.ToString(), TeamColor, player.GetComponent<Health>().Lives, player.GetComponent<setup>().myname.ToString());
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in PlayerScoreBoardList)
        {
            Destroy(child.gameObject);
        }
    }
   

}
