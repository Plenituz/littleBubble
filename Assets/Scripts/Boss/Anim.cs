using UnityEngine;
using System.Collections;


public class Anim{
	float from, to, duration, startTime;
	Interpolator interpolator;
	OnAnimationUpdateListener l;

	public Anim (float from, float to, float duration, Interpolator i, OnAnimationUpdateListener listener){
		this.from = from;
		this.to = to;
		this.duration = duration;
		interpolator = i;
		if (interpolator == null)
			interpolator = new LinearInterpolator ();
		l = listener;
	}

	public void Start(){
		startTime = Time.time * Time.timeScale;
		TickAction.actions += Animation;
	}

	void Animation(){
		while (Time.time - startTime <= duration) {
			float value = (from + (to - from)*interpolator.Interpolate((Time.time - startTime)/duration));
			if (l != null)
				l (value);
			return;
		}
		l (to);
		TickAction.actions -= Animation;
	}
}

public delegate void OnAnimationUpdateListener (float value);


public abstract class Interpolator{
	abstract public float Interpolate (float input);
}

public class LinearInterpolator : Interpolator{
	override public float Interpolate(float input){
		return input;
	}
}

public class AccelerateDeccelerateInterpolator : Interpolator{
	override public float Interpolate(float input){
		return (Mathf.Cos((input + 1) * Mathf.PI) / 2.0f) + 0.5f;
	}
}

public class DeccelerateInterpolator : Interpolator{
	private float mFactor;

	public DeccelerateInterpolator(){
		mFactor = 1.0f;
	}

	public DeccelerateInterpolator(float factor){
		mFactor = factor;
	}

	override public float Interpolate(float input){
		float result;
		if (mFactor == 1.0f) {
			result = (float)(1.0f - (1.0f - input) * (1.0f - input));
		} else {
			result = (float)(1.0f - Mathf.Pow((1.0f - input), 2 * mFactor));
		}
		return result;
	}
}

public class BounceInterpolator : Interpolator{
	
    private static float bounce(float t) {
        return t * t * 8.0f;
    }

    override public float Interpolate(float t) {
        // _b(t) = t * t * 8
        // bs(t) = _b(t) for t < 0.3535
        // bs(t) = _b(t - 0.54719) + 0.7 for t < 0.7408
        // bs(t) = _b(t - 0.8526) + 0.9 for t < 0.9644
        // bs(t) = _b(t - 1.0435) + 0.95 for t <= 1.0
        // b(t) = bs(t * 1.1226)
        t *= 1.1226f;
        if (t < 0.3535f) return bounce(t);
        else if (t < 0.7408f) return bounce(t - 0.54719f) + 0.7f;
        else if (t < 0.9644f) return bounce(t - 0.8526f) + 0.9f;
        else return bounce(t - 1.0435f) + 0.95f;
    }

}

public class AnticipateOvershootInterpolator : Interpolator{
	private float mTension;

	public AnticipateOvershootInterpolator(){
		mTension = 2.0f * 1.5f;
	}

	public AnticipateOvershootInterpolator(float tension){
		mTension = tension * 1.5f;
	}
	
    private static float a(float t, float s) {
    	return t * t * ((s + 1) * t - s);
    }

    private static float o(float t, float s) {
        return t * t * ((s + 1) * t + s);
    }

    override public float Interpolate(float t) {
        // a(t, s) = t * t * ((s + 1) * t - s)
        // o(t, s) = t * t * ((s + 1) * t + s)
        // f(t) = 0.5 * a(t * 2, tension * extraTension), when t < 0.5
        // f(t) = 0.5 * (o(t * 2 - 2, tension * extraTension) + 2), when t <= 1.0
        if (t < 0.5f) return 0.5f * a(t * 2.0f, mTension);
        else return 0.5f * (o(t * 2.0f - 2.0f, mTension) + 2.0f);
    }
}

public class OvershootInterpolator : Interpolator{
	private float mTension;

	public OvershootInterpolator(){
		mTension = 2.0f;
	}

	public OvershootInterpolator(float tension){
		mTension = tension;
	}

	override public float Interpolate(float t) {
		// _o(t) = t * t * ((tension + 1) * t + tension)
        // o(t) = _o(t - 1) + 1
        t -= 1.0f;
        return t * t * ((mTension + 1) * t + mTension) + 1.0f;
	}
}

public class AnticipateInterpolator : Interpolator{
	private float mTension;

	public AnticipateInterpolator(){
		mTension = 2.0f;
	}

	public AnticipateInterpolator(float tension){
		mTension = tension;
	}

	override public float Interpolate(float t) {
		// a(t) = t * t * ((tension + 1) * t - tension)
        return t * t * ((mTension + 1) * t - mTension);
	}
}