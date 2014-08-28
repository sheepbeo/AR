using UnityEngine;
using System.Collections;

public class LogoTrackableEventHandler : MonoBehaviour, ITrackableEventHandler 
{
	#region PRIVATE_MEMBER_VARIABLES
	
	private TrackableBehaviour _trackableBehaviour;
	
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	public LogoManager Logo;
	
	#region UNTIY_MONOBEHAVIOUR_METHODS
	
	void Start()
	{
		_trackableBehaviour = GetComponent<TrackableBehaviour>();
		if (_trackableBehaviour)
		{
			_trackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}
	
	#endregion // UNTIY_MONOBEHAVIOUR_METHODS
	
	
	
	#region PUBLIC_METHODS
	
	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	
	#endregion // PUBLIC_METHODS
	
	
	
	#region PRIVATE_METHODS
	
	
	private void OnTrackingFound()
	{
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}

		if (Logo != null) {
			Logo.StartMoving();
		}
	}
	
	
	private void OnTrackingLost()
	{
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
	}
	
	#endregion // PRIVATE_METHODS
}
