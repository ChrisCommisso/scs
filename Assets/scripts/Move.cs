using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    [Serializable]
    public class Move
    {
        
        public int hitstopFrames;
        public int currentFrame;
        public List<Sprite> frames;
        public MoveData frameData;
        public bool loop;
        public Move Copy() 
        {
            Move m = new Move();
            m.currentFrame = 0;
            m.frameData = this.frameData;
            m.frames = this.frames;
            m.loop = this.loop;
            return m;
        }
    }
}
