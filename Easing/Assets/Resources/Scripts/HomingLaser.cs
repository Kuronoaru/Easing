using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    public EasingTypes easingType { get; set; }

    [SerializeField, Header("曲がりきるまでの時間")]
    float curveTime = 0.3f;

   public GameObject targetObject { get; set; }

    float deltaTime = 0.0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime = Mathf.Clamp(deltaTime, 0.0f, curveTime);
        //現在の経過時間が曲がりきるまでの時間に対してのパーセントを求める
        var targetPercent = deltaTime / curveTime;
        //パーセントを各種イージングで補正.
        targetPercent = Easing.GetEasing(targetPercent, easingType);
        //上方向ベクトル用のパーセンテージを求める
        var axisYPercent = 1.0f - targetPercent;

        //目標へのベクトルを求める.
        var targetVector = targetObject.transform.position - transform.position;
        targetVector.Normalize();
        //現在のY軸方向を取得.
        var AxisY = transform.up;
        //上方向と目標へのベクトルを足し合わせてホーミングの挙動を作る
        var moveVector = ((targetVector * targetPercent) + (AxisY * axisYPercent)) * targetPercent;
        transform.position += moveVector;
    }

    public void ZRotate(float zAngle)
    {
        transform.Rotate(0.0f,0.0f,zAngle);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
