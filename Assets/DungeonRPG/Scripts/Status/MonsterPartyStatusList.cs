using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[Serializable]
[CreateAssetMenu(fileName = "MonsterPartyStatusList", menuName = "CreateMonsterPartyStatusList")]
public class EnemyPartyStatusList : ScriptableObject
{
    [SerializeField]
    private List<MonsterPartyStatus> partyMembersList = null;
 
    public List<MonsterPartyStatus> GetPartyMembersList() {
        return partyMembersList;
    }
}

