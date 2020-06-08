using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
 
[Serializable]
public abstract class CharacterStatus : ScriptableObject
{
 
    //　キャラクターの名前
    [SerializeField]
    private string characterName = "";
    //　キャラクターのレベル
    [SerializeField]
    private int level = 1;
    //　最大HP
    [SerializeField]
    private int maxHp = 100;
    //　HP
    [SerializeField]
    private int hp = 100;
    //　最大MP
    [SerializeField]
    private int maxMp = 50;
    //　MP
    [SerializeField]
    private int mp = 50;
 
    public void SetCharacterName(string characterName) {
        this.characterName = characterName;
    }
 
    public string GetCharacterName() {
        return characterName;
    }
 
    public void SetLevel(int level) {
        this.level = level;
    }
 
    public int GetLevel() {
        return level;
    }
 
    public void SetMaxHp(int hp) {
        this.maxHp = hp;
    }
 
    public int GetMaxHp() {
        return maxHp;
    }
 
    public void SetHp(int hp) {
        this.hp = Mathf.Max(0, Mathf.Min(GetMaxHp(), hp));
    }
 
    public int GetHp() {
        return hp;
    }
 
    public void SetMaxMp(int mp) {
        this.maxMp = mp;
    }
 
    public int GetMaxMp() {
        return maxMp;
    }
 
    public void SetMp(int mp) {
        this.mp = Mathf.Max(0, Mathf.Min(GetMaxMp(), mp));
    }
 
    public int GetMp() {
        return mp;
    }
}
