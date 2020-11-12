using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField, Header("目標")]
    GameObject targetObject = null;

    [SerializeField, Header("ホーミングプレハブ")]
    GameObject homingObject = null;

    [SerializeField, Header("発射数")] 
    float count = 10;

    [SerializeField, Header("イージングの種類")]
    EasingTypes type;
    
    [SerializeField, Header("連射速度")]
    float fireRate = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var addAngle = 360.0f / count;
            for (float angle = 0; angle < 360.0f; angle += addAngle)
            {
                Execute(angle);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            //コルーチンの呼び出し(関数名を文字列で呼び出す)
            StartCoroutine("Gatling");
        }
    }

    void Execute(float angle)
    {
        var clone = Instantiate(homingObject);
        if (clone.TryGetComponent<HomingLaser>(out var homing))
        {
            homing.ZRotate(angle);
            homing.easingType = type;
            homing.targetObject = targetObject;
            clone.transform.position = transform.position;
        }
        else
        {
            Destroy(clone);
        }
    }

    //コルーチンはIEnumeratorで指定する yield return (戻り値)が必須
    IEnumerator Gatling()
    {
        var addAngle = 360.0f / count;
        for (float angle = 0; angle < 360.0f; angle += addAngle)
        {
            Execute(angle);
            //指定秒停止して再度処理を開始する
            yield return new WaitForSeconds(fireRate);
        }
    }
}
