using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneLoadingPosition : MonoBehaviour {
    [SerializeField]
    private SceneMovementData sceneMovementData = null;
 
    void Start() {
 
        // シーン遷移の種類に応じて初期位置のゲームオブジェクトの位置と角度に設定
        if (sceneMovementData.GetSceneType() == SceneMovementData.SceneType.StartGame) {
            var initialPosition = GameObject.Find("InitialPosition").transform;
            transform.position = initialPosition.position;
            transform.rotation = initialPosition.rotation;
        } else if (sceneMovementData.GetSceneType() == SceneMovementData.SceneType.Orario) {
            var initialPosition = GameObject.Find("InitialPosition").transform;
            transform.position = initialPosition.position;
            transform.rotation = initialPosition.rotation;
        } else if (sceneMovementData.GetSceneType() == SceneMovementData.SceneType.OrarioToDungeon) {
            var initialPosition = GameObject.Find("InitialPositionOrarioToDungeon").transform;
            transform.position = initialPosition.position;
            transform.rotation = initialPosition.rotation;
        } 
        // else if (sceneMovementData.GetSceneType() == SceneMovementData.SceneType.DungeonToBattle) {
        //     var initialPosition = GameObject.Find("BattlePosition").transform;
        //     transform.position = initialPosition.position;
        //     transform.rotation = initialPosition.rotation;
        // }
    }
}