using System;

namespace Constants
{
	public enum CanvasEnum
	{
		WelcomeCanvas = 0,
		CaptureBackgroundCanvas = 1,
		SettingPatnerPositionCanvas = 2,
		CompleteBeforeSettingCanvas = 3,
		StepByStepCanvas = 4,
		ReadyExerciseCanvas = 5,
		ContinueExerciseCanvas = 6,
		StopExerciseCanvas = 7
	}

	public class Exercise
	{
		public const String Pushup = "팔굽혀펴기";
		public const String Situp = "윗몸일으키기";
		public const String SpreadArmsAndJump = "팔벌려뛰기";
	}
}

