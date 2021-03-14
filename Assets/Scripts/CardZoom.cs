using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{
  public GameObject Canvas;
  public Sprite notShow;

  private GameObject realCard;
  private GameObject zoomCard;

  public void Awake() {
    Canvas = GameObject.Find("Main Canvas");
  }

  public void onHoverEnter() {
    /*Offline method
    zoomCard = Instantiate(gameObject, new Vector2(63, 150), Quaternion.identity);
    zoomCard.transform.SetParent(Canvas.transform, true);
    zoomCard.layer = LayerMask.NameToLayer("Zoom");

    RectTransform rect = zoomCard.GetComponent<RectTransform>();
    rect.sizeDelta = new Vector2(150, 200);
    */
    realCard = gameObject;
    zoomCard = new GameObject();
    //Getting the right image
    Image cardImage = zoomCard.AddComponent<Image>();
    cardImage.sprite = gameObject.GetComponent<Image>().sprite;
    if(cardImage.sprite == notShow) return;
    //Setting the right size
    RectTransform rect = zoomCard.GetComponent<RectTransform>();
    rect.sizeDelta = new Vector2(180, 240);
    //Setting the right position
    zoomCard.transform.Translate(new Vector3(95, 170, 0));
    zoomCard.GetComponent<RectTransform>().SetParent(Canvas.transform);
    zoomCard.layer = LayerMask.NameToLayer("Zoom");
    zoomCard.SetActive(true);

    //Highlight selected card

  }

  public void onHoverExit() {
    Destroy(zoomCard);
  }
}
