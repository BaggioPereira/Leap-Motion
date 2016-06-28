﻿using UnityEngine;
using System.Collections;
using Leap;

namespace Leap.Unity
{
    public class RecordingControls : MonoBehaviour
    {
        public bool EnableRecording;
        public TextAsset recordingAsset;
        public bool looping;
        Recorder recorder;

        public string header;
        public GUIText controlsGui;
        public GUIText recordingGui;
        

        public KeyCode beginRecordingKey = KeyCode.R;
        public KeyCode endRecordingKey = KeyCode.R;
        public KeyCode beginPlaybackKey = KeyCode.P;
        public KeyCode pausePlaybackKey = KeyCode.P;
        public KeyCode stopPlaybackKey = KeyCode.S;
        // Use this for initialization
        void Start()
        {
            recorder = new Recorder();
        }

        public Recorder GetRecorder()
        {
            return recorder;
        }

        public void ResetRecording()
        {
            recorder.Reset();
        }

        public void Record()
        {
            recorder.Record();
        }

        public void PlayRecording()
        {
            recorder.Play();
        }

        public void PauseRecording()
        {
            recorder.Pause();
        }

        public void StopRecording()
        {
            recorder.Stop();
        }

        public float GetRecordingProgress()
        {
            return recorder.GetProgress();
        }

        public string FinishAndSave()
        {
            string path = recorder.SaveFile();
            recorder.Play();
            return path;
        }


        // Update is called once per frame
        void Update() 
        {
            if (controlsGui != null) controlsGui.text = header + "\n";

            switch(GetRecorder().state)
            {
                case States.Recording:
                    break;
                case States.Playing:
                    break;
                case States.Paused:
                    break;
                case States.Stopped:
                    break;
            }
	    }
    }
}