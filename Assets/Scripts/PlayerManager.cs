using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
  public GameObject Card1;
  public GameObject Card2;
  public GameObject PlayerArea;
  public GameObject EnemyArea;
  public GameObject DropZone;

  List<GameObject> cards = new List<GameObject>();

  [SyncVar]
  int allCardsPlayed = 0;
  int playerCardsPlayed = 0;
  int enemyCardsPlayed = 0;

    public override void OnStartClient()
    {
      base.OnStartClient();

      PlayerArea = GameObject.Find("_PlayerArea");
      EnemyArea = GameObject.Find("_EnemyArea");
      DropZone = GameObject.Find("_DropZone");
      Debug.Log(DropZone);
    }

    [Server]
    public override void OnStartServer() {
      cards.Add(Card1);
      cards.Add(Card2);
    }

    [Command]
    public void CmdDealCards() {
      for(int i = 0; i < 5; i++) {
        GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0,0), Quaternion.identity);
        NetworkServer.Spawn(card, connectionToClient);
        RpcShowCard(card, "Dealt");
      }
    }

    public void PlayCard(GameObject card) {
      CmdPlayCard(card);
    }

    [Command]
    public void CmdPlayCard(GameObject card) {
      RpcShowCard(card, "Played");
    }

    [ClientRpc]
    void RpcShowCard(GameObject card, string type) {
      if (type == "Dealt") {
        if (hasAuthority) {
          card.transform.SetParent(PlayerArea.transform, false);
        }
        else {
          card.transform.SetParent(EnemyArea.transform, false);
          card.GetComponent<CardFlipper>().Flip();
        }
      }
      else if (type == "Played") {
        card.transform.SetParent(DropZone.transform, false);
        playerCardsPlayed++;
        allCardsPlayed++;
        if (!hasAuthority) {
          card.GetComponent<CardFlipper>().Flip();
          enemyCardsPlayed++;
        }
        //Così non va, client sovrascrive tutto
        Debug.Log("All "+allCardsPlayed.ToString());
        Debug.Log("Player "+playerCardsPlayed.ToString());
        Debug.Log("Enemy "+enemyCardsPlayed.ToString());
      }
    }
}
