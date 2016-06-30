using UnityEngine;
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

        LeapProvider provider;

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
                    allowEndRecording();
                    break;
                case States.Playing:
                    allowPause();
                    allowStop();
                    break;
                case States.Paused:
                    allowBeginPlayback();
                    allowStop();
                    break;
                case States.Stopped:
                    allowBeginRecording();
                    allowBeginPlayback();
                    break;
            }
	    }

        private void allowBeginRecording()
        {
            if (controlsGui != null) controlsGui.text += beginRecordingKey + " -Begin Recording\n";
            if(Input.GetKeyDown(beginRecordingKey))
            {
                ResetRecording();
                Record();
                recordingGui.text = "";
            }
        }

        private void allowBeginPlayback()
        {
            if (controlsGui != null) controlsGui.text += beginPlaybackKey + " - Begin Playback\n";
            if (Input.GetKeyDown(beginPlaybackKey))
            {
                PlayRecording();
            }
        }

        private void allowEndRecording()
        {
            if (controlsGui != null) controlsGui.text += endRecordingKey + " - End Recording\n";
            if(Input.GetKeyDown(endRecordingKey))
            {
                string savePath = FinishAndSave();
                recordingGui.text = "Recording saves to:\n" + savePath;
            }
        }

        private void allowPause()
        {
            if (controlsGui != null) controlsGui.text += pausePlaybackKey + " - Paused Playback\n";
            if(Input.GetKeyDown(pausePlaybackKey))
            {
                PauseRecording();
            }
        }

        private void allowStop()
        {
            if (controlsGui != null) controlsGui.text += stopPlaybackKey + " - Stopped Playback\n";
            if(Input.GetKeyDown(stopPlaybackKey))
            {
                StopRecording();
            }
        }
    }
}
