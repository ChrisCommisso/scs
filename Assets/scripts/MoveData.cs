

using System;
using System.Collections.Generic;

namespace Assets.scripts
{
    [Serializable]
    public class MoveData
    {
        public MoveData() { 
        }
        public int animLength;//60
        public float[] gravityMult;
        public int throwInvulStartup;
        public int hitInvulStartup;
        public int armorStartup;
        public int throwInvulEnd;
        public int hitInvulEnd;
        public int armorEnd;
        public bool airOk;
        public bool groundOk;
        public bool landCancel;
        public float startUpHitStopEnemy;
        public float startUpHitStopFriendly;
        public string[] requireResource;
        public string naturalEndAnim;
        public string[] input;//"2","3","6+B"
        public string[] actions;
        public string[] canCancelTo;//"animid-frame" eg. "dash-5" can cancel to dash by frame 5 if the string contaqins a '~' at the front character it will require a hit to cancel as well
        public KaraFrame[] karaKeyFrames;
        public HitboxGroup[] hitBoxes;
        public BoxRect[] bodyBox;
        public List<BoxRect>[] hurtBoxes;//100 pixels per unit for rect sizing, hitboxes will auto scale .01 unit per pixel
    }
}
