using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopExerciseCanvas : MonoBehaviour {

    private Transform canvasListTransform;  // To grab "Canvas List" context

    GameObject ContinueExercise;
    Text text;
    float time;
    int count;
    int patnerCount;

    public bool isStopedAnimationToExercise = false;

	void Start () {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
        ContinueExercise = canvasListTransform.Find("ContinueExerciseCanvas").gameObject;
        text = canvasListTransform.Find("StopExerciseCanvas/Text").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		if (this.isActiveAndEnabled)
        {
            time = (int) ContinueExercise.GetComponent<ContinueExerciseCanvas>().time;
            count = ContinueExercise.GetComponent<ContinueExerciseCanvas>().count;
            patnerCount = ContinueExercise.GetComponent<ContinueExerciseCanvas>().patnerCount;
            text.text = "훌륭합니다!\n" + time + "초 동안 " + count + "회 하셨네요!\n" +
                "파트너는 " + patnerCount + "회 했습니다!\n" + "좀 더 횟수를 늘려볼까요?";

            if (!isStopedAnimationToExercise) {
				isStopedAnimationToExercise = true;
				GameObject.Find ("/ImageTarget/Beta")
					.GetComponent<BetaController> ().stopExerciseAnimation ();
				canvasListTransform.Find ("ReadyExerciseCanvas")
					.GetComponent<ReadyExerciseCanvas> ().isReadiedAnimationToExercise = false;
            }
        }
	}
}
