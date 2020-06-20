
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BattleManager : MonoBehaviour
{
    //　戦闘データ
    [SerializeField]
    private BattleData battleData = null;
    //　キャラクターのベース位置
    [SerializeField]
    private Transform battleBasePosition;
    //　現在戦闘に参加しているキャラクター
    private List<GameObject> allCharacterList = new List<GameObject>();
 
    // Start is called before the first frame update
    void Start() {
        Transform characterTransform;
        List<GameObject> instances = new List<GameObject>();
        GameObject ins;
        //　味方パーティーのプレハブをインスタンス化
        for (int i = 0; i < battleData.GetAllyPartyStatus().GetAllyGameObject().Count; i++) {
            characterTransform = battleBasePosition.Find("AllyPos" + i).transform;
            ins = Instantiate<GameObject>(battleData.GetAllyPartyStatus().GetAllyGameObject()[i], characterTransform.position, characterTransform.rotation);
            allCharacterList.Add(ins);
        }
        //　敵パーティーのプレハブをインスタンス化
        for (int i = 0; i < battleData.GetMonsterPartyStatus().GetMonsterGameObjectList().Count; i++) {
            characterTransform = battleBasePosition.Find("MonsterPos" + i).transform;
            ins = Instantiate<GameObject>(battleData.GetMonsterPartyStatus().GetMonsterGameObjectList()[i], characterTransform.position, characterTransform.rotation);
            allCharacterList.Add(ins);
        }
    }
}