using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EasingTest : MonoBehaviour
{
    [SerializeField, Header("目標時間(イージングの変化を確認する時間)")]
    float targetTime = 3.0f;

    [Header("イージングの種類")]
    public EasingTypes type;

    [Header("イージングの影響を受けるゲームオブジェクト=================")]
    [SerializeField] Slider     slider;
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject plane;
    [SerializeField] GameObject image;

    float deltaTime = 0.0f;     //経過時間
    float targetImageX = 1000.0f;

    void Start()
    {
    }
   
    void Update()
    {
        //前回のフレームから経過した時間を追加
        deltaTime += Time.deltaTime;
        deltaTime = Mathf.Clamp(deltaTime,0.0f, targetTime);

        float value = Easing.GetEasing(deltaTime/ targetTime, type);
        
        //スライダーの値にイージングの結果を代入する
        slider.value = value;

        cube.transform.localScale = new Vector3(value, value, value);
        sphere.transform.localScale = new Vector3(value, value, value);

        float x = value * targetImageX;
        var position = image.transform.position;
        position.x = x;
        image.transform.position = position;
    }

    public void Reset()
    {
        deltaTime = 0.0f;
    }
}
