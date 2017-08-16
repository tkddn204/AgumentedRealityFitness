using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;

public class StepByStepCanvas : MonoBehaviour {

    private Transform canvasListTransform;  // To grab "Canvas List" context
    Text text;

    private string currentExercise;
    private int currentStep = 1;
    private int maxStep;
    
    void Start()
    {
        canvasListTransform = GameObject.Find("/UI/Canvas List").transform;
        text = canvasListTransform.Find("StepByStepCanvas/Text")
            .GetComponent<Text>();
        currentExercise = GameObject.Find("/ImageTarget/Beta")
            .GetComponent<BetaController>().exercise;
        Init();
        maxStep = Exercise.GetStep(currentExercise);
    }
    void Init()
    {
        currentStep = 1;
        time = 0.0f;
    }

    bool isCompletedCurrentStep = false;
    float time = 0.0f;
    float endTime = 10.0f;

    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            time += Time.deltaTime;
            switch (currentStep)
            {
                case 1:
                    // TODO: 잠시 멈추면 카운트를 셈, 3초가 지나면 다음 동작으로
                    if (time >= endTime)
                    {
                        currentStep++;
                    }
                    break;
                case 2:
                    // TODO: 두번째 단계에서 잠깐 애니메이션을 멈춰야함
                    GameObject.Find("/ImageTarget/Beta")
                        .GetComponent<BetaController>().nextStep();
                    break;
                default:
                    GameObject.Find("/ImageTarget/Beta")
                        .GetComponent<BetaController>().endStep();
                    GameObject.Find("/ImageTarget/Beta")
                        .GetComponent<BetaController>().stopExerciseAnimation();
                    Init();
                    break;
            }

            text.text = (int)time + " / " + (int)endTime + "초 안에 자세를 잡아주세요!\n"
                + currentExercise + " " + currentStep + " / " + maxStep;
        }
    }
}
