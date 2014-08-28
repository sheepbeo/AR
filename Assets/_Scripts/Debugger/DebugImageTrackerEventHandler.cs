using UnityEngine;
using System.Collections;

public class DebugImageTrackerEventHandler : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour;
	public bool isDebugging = false;

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (isDebugging) {
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
			    newStatus == TrackableBehaviour.Status.TRACKED ||
			    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				Debugger.Debug(newStatus);
				OnTrackingFound();
			}
			else
			{
				Debugger.Debug(newStatus);
				OnTrackingLost();
			}
		}
	}

	private void OnTrackingFound()
	{
		
		Debugger.Debug("found");
	}
	
	
	private void OnTrackingLost()
	{
		Debugger.Debug("lost");
	}

}
