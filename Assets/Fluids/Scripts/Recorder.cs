using UnityEngine;
using System;
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

    public class Recorder {
        public float playbackSpeed = 1.0f;
        public bool isLooping = true;

        public States state = States.Playing;

        protected List<byte[]> frames;
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
            frames = new List<byte[]>();
            frameIndex = 0.0f;
        }

        public float GetProgress()
        {
            return frameIndex / frames.Count;
        }

        public int GetIndex()
        {
            return (int)frameIndex;
        }

        public void AddFrame(Frame frame)
        {
            frames.Add(frame.Serialize);
        }

        public Frame GetCurrentFrame()
        {
            return currentFrame;
        }

        public Frame NextFrame()
        {
            currentFrame = new Frame();
            if(frames.Count > 0)
            {
                if(frameIndex >= frames.Count && isLooping)
                {
                    frameIndex -= frames.Count;
                }
                else if(frameIndex < 0 && isLooping)
                {
                    frameIndex += frames.Count;
                }
                if(frameIndex < frames.Count && frameIndex >=0)
                {
                    currentFrame.Deserialize(frames[(int)frameIndex]);
                    frameIndex += playbackSpeed;
                }
            }
            return currentFrame;
        }

        public List<Frame> GetFrames()
        {
            List<Frame> newFrames = new List<Frame>();
            for(int i = 0; i < frames.Count; ++i)
            {
                Frame frame = new Frame();
                frame.Deserialize(frames[i]);
                newFrames.Add(frame);
            }
            return newFrames;
        }

        public int GetFramesCount()
        {
            return frames.Count;
        }

        public string SaveFile()
        {
            string path = "Assets/Recordings/" + System.DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";
            return SaveFile(path);
        }

        public string SaveFile(string path)
        {
            if(File.Exists(@path))
            {
                File.Delete(@path);
            }
            FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
            for(int i = 0; i < frames.Count; ++i)
            {
                byte[] frameSize = new byte[4];
                frameSize = System.BitConverter.GetBytes(frames[i].Length);
                stream.Write(frameSize, 0, frameSize.Length);
                stream.Write(frames[i], 0, frames[i].Length);
            }
            stream.Close();
            return path;
        }

        public void Load(TextAsset textAsset)
        {
            Load(textAsset.bytes);
        }

        public void Load(byte[] data)
        {
            frameIndex = 0;
            frames.Clear();
            int i = 0;
            while (i < data.Length)
            {
                byte[] frameSize = new byte[4];
                Array.Copy(data, i, frameSize, 0, frameSize.Length);
                i += frameSize.Length;
                byte[] frame = new byte[System.BitConverter.ToUInt32(frameSize, 0)];
                Array.Copy(data, i, frame, 0, frame.Length);
                i += frame.Length;
                frames.Add(frame);
            }
        }
    }
}