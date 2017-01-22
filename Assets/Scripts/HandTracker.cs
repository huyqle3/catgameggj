using Academy.HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

/// <summary>
/// HandsManager determines if the hand is currently detected or not.
/// </summary>
public partial class HandTracker : Singleton<HandTracker>
    {
        /// <summary>
        /// HandDetected tracks the hand detected state.
        /// Returns true if the list of tracked hands is not empty.
        /// </summary>
        public bool HandDetected
        {
            get { return trackedHands.Count > 0; }
        }

        public GameObject TrackingObject;

        private HashSet<uint> trackedHands = new HashSet<uint>();
        private Dictionary<uint, GameObject> trackingObject = new Dictionary<uint, GameObject>();

        void Awake()
        {
            InteractionManager.SourceDetected += InteractionManager_SourceDetected;
            InteractionManager.SourceLost += InteractionManager_SourceLost;
            InteractionManager.SourceUpdated += InteractionManager_SourceUpdated;
        }

        private void InteractionManager_SourceUpdated(InteractionSourceState state)
        {
            uint id = state.source.id;
            Vector3 pos;

            if (state.source.kind == InteractionSourceKind.Hand)
            {
                if (trackingObject.ContainsKey(state.source.id))
                {
                    if (state.properties.location.TryGetPosition(out pos))
                    {
                        trackingObject[state.source.id].transform.position = pos + ((Camera.main.transform.forward * 0.75f + Vector3.forward * 0.25f) * 2f);
                    }
                }
            }

        }

        private void InteractionManager_SourceDetected(InteractionSourceState state)
        {
            // Check to see that the source is a hand.
            if (state.source.kind != InteractionSourceKind.Hand)
            {
                return;
            }
            trackedHands.Add(state.source.id);

            var obj = Instantiate(TrackingObject) as GameObject;
            Vector3 pos;
            if (state.properties.location.TryGetPosition(out pos))
            {
                obj.transform.position = pos;
            }

            trackingObject.Add(state.source.id, obj);
        }

        private void InteractionManager_SourceLost(InteractionSourceState state)
        {
            // Check to see that the source is a hand.
            if (state.source.kind != InteractionSourceKind.Hand)
            {
                return;
            }

            if (trackedHands.Contains(state.source.id))
            {
                trackedHands.Remove(state.source.id);
            }

            if (trackingObject.ContainsKey(state.source.id))
            {
                var obj = trackingObject[state.source.id];
                trackingObject.Remove(state.source.id);
                Destroy(obj);
            }
        }

        void OnDestroy()
        {
            InteractionManager.SourceDetected -= InteractionManager_SourceDetected;
            InteractionManager.SourceLost -= InteractionManager_SourceLost;
            InteractionManager.SourceUpdated -= InteractionManager_SourceUpdated;
        }
    }