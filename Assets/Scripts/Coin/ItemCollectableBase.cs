using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
   
   public string compareTag = "Player";

   private void OnTriggerEnter2D(Collider2D collision)
   {
      Debug.Log($"Trigger com: {collision.name}");
      if (collision.transform.CompareTag(compareTag))
      {
         CollectItem();
      }
   }

   protected virtual void CollectItem()
   {
      gameObject.SetActive(false);
      OnCollectItem();
   }

   protected virtual void OnCollectItem() {}
}
