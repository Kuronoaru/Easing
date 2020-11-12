using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineTest : MonoBehaviour
{
    enum StateType : int
    {
        A,
        B,
        C,
    }

    StateMachine<StateType> stateMachine = new StateMachine<StateType>();
    [SerializeField] float time;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.Add(StateType.A, 
        //Updateラムダ
        ()=> 
        {
            time += Time.deltaTime;
            if (time > 3.0f)
            {
                stateMachine.ChangeState(StateType.B);
            }
        },
        //Enterラムダ
        ()=>
        {
            Debug.LogFormat($"EnterState = {StateType.A}");
            //Debug.LogFormat("EnterState = {0}",State.A);
            //printf("EnterState = %d",State.A);
        },
        //Exitラムダ
        ()=>
        {
            Debug.LogFormat($"ExitState = {StateType.A}");
        });
 
        stateMachine.Add(StateType.B, 
        //Updateラムダ
        () =>
        {
            time += Time.deltaTime;
            if (time > 5.0f)
            {
                stateMachine.ChangeState(StateType.C);
            }
        });

        stateMachine.Add(StateType.C,
        //Updateラムダ
        () =>
        {
            time += Time.deltaTime;
            if (time > 2.0f)
            {
                stateMachine.ChangeState(StateType.A);
            }
        }
        //Enterラムダ
        ,() =>
        {
            Debug.LogFormat($"EnterState = {StateType.C}");
            time = 0.0f;
        },
        //Exitラムダ
        () =>
        {
            Debug.LogFormat($"ExitState = {StateType.C}");
            time = 0.0f;
        });
        stateMachine.ChangeState(StateType.A);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
