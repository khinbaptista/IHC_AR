using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vuforia;

public class AREventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
	[SerializeField]
	private string targetName = "Image Target";
	[SerializeField]
	private ImageTargetBehaviour _targetTemplate;

	private UserDefinedTargetBuildingBehaviour _targetBuilder;
	private ObjectTracker _objectTracker;
	private DataSet _dataSet;
	private ImageTargetBuilder.FrameQuality _frameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;


	void Start () {
		_targetBuilder = GetComponent<UserDefinedTargetBuildingBehaviour>();

		if (_targetBuilder != null)
			_targetBuilder.RegisterEventHandler(this);
	}
	
	void Update () {
	
	}

	public void OnInitialized() {
		_objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

		if (_objectTracker != null) {
			_dataSet = _objectTracker.CreateDataSet();
			_objectTracker.ActivateDataSet(_dataSet);
		}
	}

	public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality) {
		_frameQuality = frameQuality;
	}

	public void OnNewTrackableSource(TrackableSource trackableSource) {
		_objectTracker.DeactivateDataSet(_dataSet);

		_dataSet.DestroyAllTrackables(true);

		ImageTargetBehaviour targetCopy = (ImageTargetBehaviour)Instantiate(_targetTemplate);
		targetCopy.gameObject.name = targetName;

		_dataSet.CreateTrackable(trackableSource, targetCopy.gameObject);
		_objectTracker.ActivateDataSet(_dataSet);
	}

	public void AddNewTrackableSource() {
		_targetBuilder.BuildNewTarget(targetName, _targetTemplate.GetSize().x);
	}
}
