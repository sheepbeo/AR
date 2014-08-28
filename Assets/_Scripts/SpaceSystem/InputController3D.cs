using UnityEngine;
using System.Collections;

public class InputController3D : MonoBehaviour 
{
	private enum InputPhase 
	{
		Begin, Moving, End, Uninitialized
	}

	public delegate void PositionInputEvent( Vector3 pos );
	public delegate void InputEvent();
	public event PositionInputEvent OnInputBegin = delegate{};
	public event PositionInputEvent OnInputMoving = delegate{};
	public event PositionInputEvent OnInputEnd = delegate{};
	
	private InputPhase _inputPhase;
	private Vector3 _inputPosition;

	public bool useClippingPlanePos = true;
	public float inputPlaneDistance = 2.0f;

	private bool _isEnabled = true;
	public bool isEnabled 
	{
		get { return _isEnabled; }
		set { _isEnabled = value; }
	}

	void Start () 
	{
		_inputPhase = InputPhase.Uninitialized;
	}

	void Update () 
	{
		if (_isEnabled && !isOverUI() ) {
#if UNITY_EDITOR
			resolveInputPhase(Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.GetMouseButtonUp(0));
			processInputPhase();
#elif (UNITY_ANDROID || UNITY_IPHONE)
			if( Input.touchCount > 0 )
			{
				resolveInputPhase(Input.GetTouch(0).phase == TouchPhase.Began
				                  ,Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary
				                  ,Input.GetTouch(0).phase == TouchPhase.Ended);
				processInputPhase();
			}
#else
			resolveInputPhase(Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.GetMouseButtonUp(0));
			processInputPhase();
#endif
		}

	}

	private bool isOverUI() 
	{
		//return (UICamera.hoveredObject != null);
		return false;
	}

	/// <summary>
	/// Resolves the input phase base on handling "unexpected" input bools
	/// </summary>
	private void resolveInputPhase(bool inputStart, bool inputStay, bool inputRelease) 
	{
		if (inputStart && !inputRelease &&_inputPhase != InputPhase.Begin ) 
		{
			_inputPhase = InputPhase.Begin;
		}

		if (inputStay && !inputStart && !inputRelease  &&  _inputPhase != InputPhase.Moving) 
		{
			_inputPhase = InputPhase.Moving;
		}

		if (inputRelease && _inputPhase != InputPhase.End) 
		{
			_inputPhase = InputPhase.End;
		}
		
		if (!inputStart && !inputStay && !inputRelease && _inputPhase == InputPhase.End) 
		{
			_inputPhase = InputPhase.Uninitialized;
		}
	}

	private void processInputPhase() 
	{
		switch (_inputPhase)
		{
		case InputPhase.Begin:
			_inputPosition = convertWorldPosition(getInputPosition());
			OnInputBegin( _inputPosition );
			break;
			
		case InputPhase.Moving:
			_inputPosition = convertWorldPosition(getInputPosition());
			OnInputMoving( _inputPosition );
			break;
			
		case InputPhase.End:
			_inputPosition = convertWorldPosition(getInputPosition());
			OnInputEnd( _inputPosition );
			break;
			
		default:
			break;
		}
	}

	private Vector3 convertWorldPosition(Vector3 screenPosition) 
	{
		Vector3 result;

		if( Camera.main.orthographic )
		{
			result = Camera.main.ScreenToWorldPoint(screenPosition);
			result.z = 0;
		}
		else
		{
			/*
			Ray ray = Camera.main.ScreenPointToRay(screenPosition);
			Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
			float distance;
			xy.Raycast(ray, out distance);
			result =  ray.GetPoint(distance);
			*/

			if (useClippingPlanePos) {
				screenPosition.z = Camera.main.nearClipPlane;
				result = Camera.main.ScreenToWorldPoint(screenPosition);
			} else {
				screenPosition.z = inputPlaneDistance;
				result = Camera.main.ScreenToWorldPoint(screenPosition);
			}

		}
		
		return result;
	}

	private Vector3 getInputPosition() 
	{
#if UNITY_EDITOR
		return Input.mousePosition;
#elif (UNITY_ANDROID || UNITY_IPHONE)
		return Input.GetTouch(0).position;
#else
		return Input.mousePosition;
#endif
	}

	private void OnLevelPauseEventHandler() 
	{
		_isEnabled = false;
	}
	
	private void OnLevelPlayEventHandler() 
	{
		_isEnabled = true;
	}
}
