using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Leap;

namespace Leap.Unity
{
    public enum States
    {
        Recording = 0,
        Playing = 1,
        Paused = 2,
        Stopped = 4,
    }

    public class Recorder : MonoBehaviour {
        public float playbackSpeed = 1.0f;
        public bool isLooping = true;

        public States state = States.Playing;

        protected List<string> frames;
        protected float frameIndex;
        protected Frame currentFrame = new Frame();

        public Recorder()
        {
            Reset();
        }
        
        public void Stop()
        {
            state = States.Stopped;
            frameIndex = 0.0f;
        }

        public void Pause()
        {
            state = States.Paused;
        }

        public void Play()
        {
            state = States.Playing;
        }

        public void Record()
        {
            state = States.Recording;
        }

        public void Reset()
        {
            frames = new List<string>();
            frameIndex = 0.0f;
        }

        public void AddFrame(Frame frame)
        {
            frames.Add(System.Text.Encoding.UTF8.GetString(frame.Serialize));
        }
    }
}

