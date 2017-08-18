using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueExerciseCanvas : MonoBehaviour {

	public bool isContinuedAnimationToExercise = false;

	private Transform canvasListTransform;  // To grab "Canvas List" context
    private Text text;

    public float time;
    public int count;

    public int patnerCount;

    void Start () {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
        text = canvasListTransform.Find("ContinueExerciseCanvas/Text").GetComponent<Text>();
        Init();
	}

	// Update is called once per frame
	void Update () {
		if (this.isActiveAndEnabled)
        {
            time += Time.deltaTime;
            text.text = "시간 : " + (int)time + "초" +
                "     " + "횟수 : " + count + "\n"
                + "파트너의 운동 횟수 : " + patnerCount;

            if (!isContinuedAnimationToExercise) {
				isContinuedAnimationToExercise = true;
                GameObject.Find("/ImageTarget/Beta")
                    .GetComponent<BetaController>().continueExerciseAnimation();
                canvasListTransform.Find ("StopExerciseCanvas")
					.GetComponent<StopExerciseCanvas> ().isStopedAnimationToExercise = false;
            }
        }
    }
    public void Init()
    {
        time = 0.0f;
        count = 0;
        patnerCount = 0;
    }
}
