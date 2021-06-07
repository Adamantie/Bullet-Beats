/// ---------------------------------------------
/// Rhythm Timeline
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.RhythmTimeline.Core.Notes
{
    using Dypsloom.RhythmTimeline.Core.Input;
    using Dypsloom.RhythmTimeline.Core.Playables;
    using UnityEngine;

    public class HeldNotePositions : Note
    {
        [Tooltip("The Start note when the input should be pressed.")]
        [SerializeField] public Transform m_StartNote;
        [Tooltip("The end note when the input should be released.")]
        [SerializeField] protected Transform m_EndNote;

        public bool stopped = false;

        public override void Initialize(RhythmClipData rhythmClipData)
        {
            base.Initialize(rhythmClipData);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void TimelineUpdate(double globalClipStartTime, double globalClipEndTime)
        {
            base.TimelineUpdate(globalClipStartTime, globalClipEndTime);
        }
    
        protected override void HybridUpdate(double timeFromStart, double timeFromEnd)
        {
            if(Application.isPlaying && (m_ActiveState == ActiveState.PostActive || m_ActiveState == ActiveState.Disabled)){return;}

            var deltaTStart = (float)(timeFromStart - m_RhythmClipData.RhythmDirector.HalfCrochet / 200f);
            var deltaTEnd =  (float)(timeFromEnd + m_RhythmClipData.RhythmDirector.HalfCrochet / 200f);

            if (stopped == false)
            {
                m_StartNote.position = GetNotePosition(deltaTStart);
            }

            m_EndNote.position = GetNotePosition(deltaTEnd);
        }
    
        protected Vector3 GetNotePosition(float deltaT)
        {
            var direction = RhythmClipData.TrackObject.GetNoteDirection(deltaT);
            var distance = deltaT * m_RhythmClipData.RhythmDirector.NoteSpeed;
            var targetPosition = m_RhythmClipData.TrackObject.EndPoint.position;
        
            return targetPosition + (direction * distance);
        }

        public override void OnTriggerInput(InputEventData inputEventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}