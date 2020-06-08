﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager loadSceneManager;
    //　シーン移動に関するデータファイル
    [SerializeField]
    private SceneMovementData sceneMovementData = null;
    //　フェードプレハブ
    [SerializeField]
    private GameObject fadePrefab = null;
    //　フェードインスタンス
    private GameObject fadeInstance;
    //　フェードの画像
    private Image fadeImage;
    
    [SerializeField]
    private float fadeSpeed = 5f;

    // シーン遷移中かどうか
    private bool isTransition;
 
    private void Awake() {
        // LoadSceneMangerは常に一つだけにする
        if(loadSceneManager == null) {
            loadSceneManager = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    //　次のシーンを呼び出す
    public void GoToNextScene(SceneMovementData.SceneType scene) {
        isTransition = true;
        // そのシーンのキャラクター座標を設定
        sceneMovementData.SetSceneType(scene);
        // コルーチン開始
        StartCoroutine(FadeAndLoadScene(scene));
    }
    //　フェードをした後にシーン読み込み
    IEnumerator FadeAndLoadScene(SceneMovementData.SceneType scene) {
        //　フェードUIのインスタンス化
        fadeInstance = Instantiate<GameObject>(fadePrefab);
        fadeImage = fadeInstance.GetComponentInChildren<Image>();
        //　フェードアウト処理
        yield return StartCoroutine(Fade(1f));
 
        //　シーンの読み込み
        if (scene == SceneMovementData.SceneType.Orario) {
            yield return StartCoroutine(LoadScene("OrarioMap"));
        } else if (scene == SceneMovementData.SceneType.OrarioToDungeon) {
            yield return StartCoroutine(LoadScene("DungeonMap18"));
        } else if (scene == SceneMovementData.SceneType.DungeonToBattle) {
            yield return StartCoroutine(LoadScene("Battle"));
        }
 
        //　フェードUIのインスタンス化
        fadeInstance = Instantiate<GameObject>(fadePrefab);
        fadeImage = fadeInstance.GetComponentInChildren<Image>();
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
 
        //　フェードイン処理
        yield return StartCoroutine(Fade(0f));
 
        Destroy(fadeInstance);
    }
    //　フェード処理
    IEnumerator Fade(float alpha) {
        var fadeImageAlpha = fadeImage.color.a;
 
        while (Mathf.Abs(fadeImageAlpha - alpha) > 0.01f) {
            fadeImageAlpha = Mathf.Lerp(fadeImageAlpha, alpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = new Color(0f, 0f, 0f, fadeImageAlpha);
            yield return null;
        }
    }
    //　実際にシーンを読み込む処理
    IEnumerator LoadScene(string sceneName) {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
 
        // シーンが完了するまで1フレーム待つを繰り返す
        while (!async.isDone) {
            yield return null;
        }
    }

    public bool IsTransition() {
        return isTransition;
    }
}