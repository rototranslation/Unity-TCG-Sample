using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
  public PlayerManager PlayerManager;

    public void onClick(){
      NetworkIdentity networkIdentity = NetworkClient.connection.identity;
      PlayerManager = networkIdentity.GetComponent<PlayerManager>();
      PlayerManager.CmdDealCards();
    }
}
