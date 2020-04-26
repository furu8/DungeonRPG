using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[Serializable]
[CreateAssetMenu(fileName = "SceneMovementData", menuName = "CreateSceneMovementData")]

public class SceneMovementData : ScriptableObject
{
    // シーンの遷移の列挙体
    public enum SceneType {
        StartGame,
        Orario,
        OrarioToDungeon
    }
    [SerializeField]
    private SceneType sceneType;

    // sceneTypeの初期化 
    public void OnEnable() {
        sceneType = SceneType.StartGame;
    }

    // シーンタイプを設定
    public void SetSceneType(SceneType scene) {
        sceneType = scene;
    }
 
    // シーンタイプを返却
    public SceneType GetSceneType() {
        return sceneType;
    }
}
 