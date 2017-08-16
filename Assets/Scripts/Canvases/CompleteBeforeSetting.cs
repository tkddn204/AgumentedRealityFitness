using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteBeforeSetting : MonoBehaviour {

    public bool isBeforeSettingAnimationToExercise = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            if (!isBeforeSettingAnimationToExercise)
            {
                isBeforeSettingAnimationToExercise = true;
                GameObject.Find("/ImageTarget/Beta")
                    .GetComponent<BetaController>().readyExeciseAnimation();
            }
        }
    }
}
