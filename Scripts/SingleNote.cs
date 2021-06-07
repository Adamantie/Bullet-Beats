/// ---------------------------------------------
/// Rhythm Timeline
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.RhythmTimeline.Core.Notes
{
    using UnityEngine;
    using Dypsloom.RhythmTimeline.Core.Input;
    using Dypsloom.RhythmTimeline.Core.Playables;

    /// <summary>
    /// The Tap Note detects a single press input.
    /// </summary>
    public class SingleNote : Note
    {
        /// <summary>
        /// The note is initialized when it is added to the top of a track.
        /// </summary>
        /// <param name="rhythmClipData">The rhythm clip data.</param>
        public override void Initialize(RhythmClipData rhythmClipData)
        {
            base.Initialize(rhythmClipData);
        }
        
        /// <summary>
        /// Reset when the note is returned to the pool.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }

        protected override void DeactivateNote()
        {
            base.DeactivateNote();
        }

        /// <summary>
        /// Hybrid Update is updated both in play mode, by update or timeline, and edit mode by the timeline. 
        /// </summary>
        /// <param name="timeFromStart">The time from reaching the start of the clip.</param>
        /// <param name="timeFromEnd">The time from reaching the end of the clip.</param>
        protected override void HybridUpdate(double timeFromStart, double timeFromEnd)
        {
            //Compute the perfect timing.
            var perfectTime = m_RhythmClipData.RealDuration / 200f;
            var deltaT = (float)(timeFromStart - perfectTime);

            //Compute the position of the note using the delta T from the perfect timing.
            //Here we use the direction of the track given at delta T.
            //You can easily curve all your notes to any trajectory, not just straight lines, by customizing the TrackObjects.
            //Here the target position is found using the track object end position.
            var direction = RhythmClipData.TrackObject.GetNoteDirection(deltaT);
            var distance = deltaT * m_RhythmClipData.RhythmDirector.NoteSpeed;
            var targetPosition = m_RhythmClipData.TrackObject.EndPoint.position;
        
            //Using those parameters we can easily compute the new position of the note at any time.
            var newPosition = targetPosition + (direction * distance);
            transform.position = newPosition;
        }

        public override void OnTriggerInput(InputEventData inputEventData)
        {
            //throw new System.NotImplementedException();
        }
    }
}