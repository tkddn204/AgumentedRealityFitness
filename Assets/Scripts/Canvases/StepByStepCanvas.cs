using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using OpenCV;
using Constants;

public class StepByStepCanvas : MonoBehaviour {
    
    private Transform canvasListTransform;  // To grab "Canvas List" context

    private AudioSource okay;
    private Text text;

    float time = 0.0f;
    float endTime = 6.0f;

    private string currentExercise;
    private int currentStep = 1;
    private int maxStep;
    
    void Start()
    {
        canvasListTransform = GameObject.Find("/UI/Canvas List").transform;
        text = canvasListTransform.Find("StepByStepCanvas/Text")
            .GetComponent<Text>();

        okay = GetComponent<AudioSource>();
        okay.Stop();

        Init();

        currentExercise = GameObject.Find("/ImageTarget/Beta")
            .GetComponent<BetaController>().exercise;
        maxStep = Exercise.GetStep(currentExercise);
    }
    
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            text.text = (int)time + " / " + (int)(endTime-1) + "초 안에 자세를 잡아주세요!\n"
                + currentExercise + "  " + currentStep + " / " + maxStep;

            switch (currentStep)
            {
                case 1:
                    time += Time.deltaTime;
                    if (time >= endTime)
                    {
                        OpenCVImage.Instance().stepOne = true;
                        nextCurrentStep();
                        okay.PlayOneShot(okay.clip);

                        GameObject.Find("/ImageTarget/Beta")
                            .GetComponent<BetaController>().nextStep();
                    }
                    break;
                case 2:
                    time += Time.deltaTime;
                    if (time >= endTime)
                    {
                        OpenCVImage.Instance().stepTwo = true;
                        nextCurrentStep();
                        okay.PlayOneShot(okay.clip);
                    }
                    break;
                case 3:
                    text.text = "잘하셨습니다!";
                    if (!okay.isPlaying)
                    {
                        Init();
                        GameObject.Find("/ImageTarget/Beta")
                            .GetComponent<BetaController>().endStep();
                        GameObject.Find("/ImageTarget/Beta")
                            .GetComponent<BetaController>().stopExerciseAnimation();
                        GameObject.Find("/Managers/Canvas Manager")
                            .GetComponent<CanvasManager>().nextCanvas();
                    }
                    break;
                default:
                    break;
            }
        }
    }
    void Init()
    {
        currentStep = 1;
        time = 0.0f;
    }
    void nextCurrentStep()
    {
        currentStep++;
        time = 0.0f;
    }
}
