using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[Serializable]
[CreateAssetMenu(fileName = "MonsterPartyStatus", menuName = "CreateMonsterPartyStatus")]
public class MonsterPartyStatus : ScriptableObject
{
    [SerializeField]
    private string partyName = null;
    [SerializeField]
    private List<GameObject> partyMembers = null;
 
    public string GetPartyName() {
        return partyName;
    }
 
    public List<GameObject> GetMonsterGameObjectList() {
        return partyMembers;
    }
}