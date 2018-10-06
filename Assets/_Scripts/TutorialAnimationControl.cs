using UnityEngine;
using System.Collections;

public class TutorialAnimationControl : MonoBehaviour
{
    public AnimationClip[] animations;
    private int currentIndex = 0;
    bool isPlaying = false;
    bool isTutorial = false;
    float _startTime = 0F;
    float _progressTime = 0F;
    float _timeAtLastFrame = 0F;
    float _timeAtCurrentFrame = 0F;
    float deltaTime = 0F;
    AnimationState _currState;

	void Start(){
		StartTutorial ();
	}

    IEnumerator UpdateCoroutine ()
    {
        _timeAtLastFrame = Time.realtimeSinceStartup;
        while (isTutorial) {
            _timeAtCurrentFrame = Time.realtimeSinceStartup;
            deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
            _timeAtLastFrame = _timeAtCurrentFrame; 

            if (isPlaying) {
                _progressTime += deltaTime;
                _currState.normalizedTime = _progressTime / _currState.length; 
                animation.Sample ();

                if (_progressTime >= _currState.length) {
                    _currState.enabled = false;
                    isPlaying = false;
                    PlayNext();
                }
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    void PlayAnimation (string suffix)
    {
        _progressTime = 0F;
        _currState = animation [suffix];
        _currState.weight = 1;
        _currState.blendMode = AnimationBlendMode.Blend;
        _currState.wrapMode = WrapMode.Once;
        _currState.normalizedTime = 0;
        _currState.enabled = true;
        isPlaying = true;
    }

    public void StartTutorial ()
    {
        isTutorial = true;
        PlayAnimation (animations [0].name);
        StartCoroutine (UpdateCoroutine ());
    }

    public void StopTutorial ()
    {
        isTutorial = false;
    }

    public void PlayNext ()
    {
        PlayAnimation (animations [(currentIndex++) % animations.Length].name);
    }
}