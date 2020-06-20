using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[Serializable]
[CreateAssetMenu(fileName = "BattleData", menuName = "CreateBattleData")]
public class BattleData : ScriptableObject
{
    //　味方パーティーデータ
    [SerializeField]
    private BattlePartyStatus battlePartyStatus;
    //　敵パーティーデータ
    [SerializeField]
    private MonsterPartyStatus monsterPartyStatus;
 
    public void SetAllyPartyStatus(BattlePartyStatus partyStatus) {
        battlePartyStatus = partyStatus;
    }
 
    public BattlePartyStatus GetAllyPartyStatus() {
        return battlePartyStatus;
    }
 
    public void SetMonsterPartyStatus(MonsterPartyStatus monsterPartyStatus) {
        this.monsterPartyStatus = monsterPartyStatus;
    }
 
    public MonsterPartyStatus GetMonsterPartyStatus() {
        return monsterPartyStatus;
    }
}