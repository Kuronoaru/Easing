using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{

    NavMeshAgent navMeshAgent = null;
    Animator animator = null;

    bool isJamp;
    float jumpTime = 0.0f;

    //　オフメッシュリンクデータ
    private OffMeshLinkData offMeshLinkData;
    //　オフメッシュリンクのスタート位置
    private Vector3 startPos;
    //　オフメッシュリンクのエンド位置
    private Vector3 endPos;

    [SerializeField] GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.isOnOffMeshLink)
        {
            //ジャンプ初期化処理
            if (isJamp == false)
            {
                //ジャンプアニメーション再生
                animator.Play("DogJump", 0);
                //ジャンプ時間初期化
                jumpTime = 0.0f;
                isJamp = true;
                //オフメッシュリンク情報取得
                offMeshLinkData = navMeshAgent.currentOffMeshLinkData;
                startPos = offMeshLinkData.startPos;
                endPos = offMeshLinkData.endPos;
            }

            jumpTime += Time.deltaTime;

            //現在の経過時間により座標を求める(線形補間)
            var nowPosition = Vector3.Lerp(startPos, endPos, jumpTime / 1.0f);
            
            //ジャンプの山なり起動を作る(1秒間に山なりの数字を出したい場合は3辺りを掛けるとそれっぽくなる)
            nowPosition.y += Mathf.Sin(jumpTime*3);
            navMeshAgent.transform.position = nowPosition;

            //指定した秒数を超えたらジャンプ移動終了
            if (jumpTime > 1.0f)
            {
                //座標は終着地点に移動させる
                navMeshAgent.transform.position = endPos;
                //Agentにオフメッシュリンクの移動が済んだことを伝える
                navMeshAgent.CompleteOffMeshLink();
                isJamp = false;
                //再追跡
                Chase();
            }
        }
    }

    public void Chase()
    {
        animator.Play("DogWalk", 0);
        //ターゲットの座標を設定し経路を計算する
        navMeshAgent.SetDestination(target.transform.position);
    }
}
