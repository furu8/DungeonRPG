using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationScopeController : MonoBehaviour
{
    // トリガーオブジェクトに侵入している間呼び出される
    void OnTriggerStay(Collider col) 
    {
        if (col.tag == "Player"
            && col.GetComponent<PlayerController>().GetState() != PlayerController.State.Talk) {
            //　ユニティちゃんが近づいたら会話相手として自分のゲームオブジェクトを渡す
            col.GetComponent<PlayerTalkDirector>().SetConversationPartner(transform.parent.gameObject);
        }
    }
 
    // トリガーオブジェクトから脱出した瞬間呼び出される
    void OnTriggerExit(Collider col) 
    {
        if (col.tag == "Player"
            && col.GetComponent<PlayerController>().GetState() != PlayerController.State.Talk) {
            //　ユニティちゃんが遠ざかったら会話相手から外す
            col.GetComponent<PlayerTalkDirector>().ResetConversationPartner(transform.parent.gameObject);
        }
    }
}
