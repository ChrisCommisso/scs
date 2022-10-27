using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MoveCreator : MonoBehaviour
{
    public int animLength
    {
        get {
            if (frames!=null) {
                return frames.Count; 
            }
            return 0;
        }
    }
    public string animname;
    public string charactername;
    public bool serialize;
    //public bool 
    public bool compileHitboxes;
    public bool compileHurtboxes;
    public bool compileBodyboxes;
    public bool compileKarakey;
    public bool compileSprites;
    
    public int currentframe;
    public int activefor;
    public defenseType blockType;
    public int blockstun;
    public int hitstun;

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
    public KaraFrame[] velocities;
    public KaraFrame[] karaKeyFrames;
    public HitboxGroup[] hitBoxes;
    public int hitstopFrames;
    public int currentFrame;
    public List<Sprite> frames;
    public bool loop;
    public BoxRect[] bodyBox;
    public List<BoxRect>[] hurtBoxes;//100 pixels per unit for rect sizing, hitboxes will auto scale .01 unit per pixel
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        if (serialize) { 
            Move move = new Move();
            string movepath = Directory.GetCurrentDirectory() + "\\Assets\\Resources\\Characters\\"+charactername+"\\"+animname;
            if (!Directory.Exists(movepath)) {
                string charPath = Directory.GetCurrentDirectory() + "\\Assets\\Resources\\Characters\\" + charactername;
                if (!Directory.Exists(charPath))//charpath
                { 
                Directory.CreateDirectory(charPath);
                }
                Directory.CreateDirectory(movepath);
            }
            move.frames = frames;
            move.loop = loop;
            move.hitstopFrames = hitstopFrames;
            move.currentFrame = 0;
            move.frameData = new MoveData();

            move.frameData.karaKeyFrames = karaKeyFrames;
            move.frameData.hitBoxes = hitBoxes;
            move.frameData.gravityMult = gravityMult;
            move.frameData.hurtBoxes = hurtBoxes;
            move.frameData.throwInvulStartup = throwInvulStartup;
            move.frameData.input=input;
            move.frameData.actions=actions;
            move.frameData.animLength = frames.Count;
            move.frameData.airOk = airOk;
            move.frameData.groundOk = groundOk;
            move.frameData.armorEnd = armorEnd;
            move.frameData.armorStartup = armorStartup;
            move.frameData.canCancelTo=canCancelTo;
            move.frameData.startUpHitStopEnemy = startUpHitStopEnemy;
            move.frameData.startUpHitStopFriendly = startUpHitStopFriendly;
            move.frameData.landCancel = landCancel;
            move.frameData.naturalEndAnim = naturalEndAnim;
            move.frameData.requireResource = requireResource;
            move.frameData.animLength=animLength;
            move.frameData.bodyBox = bodyBox;
            if (bodyBox.Length != frames.Count) {
                throw new System.Exception("please compile bodyboxes for EACH frame");
            }
            int i = 0;
            foreach (Sprite s in frames) {
                StreamWriter sw = new StreamWriter(movepath+"\\"+animname+i+".png");
                byte[] file = s.texture.EncodeToPNG();
                sw.Write(file);
                sw.Flush();
                sw.Close();
                i++;
            }
            

            string jsonoutput = JsonUtility.ToJson(move);
            StreamWriter streamWriter = new StreamWriter(movepath + "\\" + animname + ".json");
            streamWriter.Write(jsonoutput);
            streamWriter.Flush();
            streamWriter.Close();
            serialize = false;
        }
        if (compileHitboxes)
        {

            BoxCollider2D[] boxes = GetComponentsInChildren<BoxCollider2D>();
            List<HitboxGroup> hboxes = hitBoxes.ToList();
            HitboxGroup group = new HitboxGroup();
            foreach (BoxCollider2D b in boxes)
            {
                if (b.enabled == true)
                    group.boxes.Add(new BoxRect(b.bounds.min.x, b.bounds.min.y, b.bounds.max.x, b.bounds.max.y));
            }
            group.hitstun = hitstun;
            group.blockstun = blockstun;
            group.velocites = velocities;
            group.blockType = blockType;
            group.activeFor = activefor;
            group.activeOn = currentFrame;
            hboxes.Add(group);
            hitBoxes = hboxes.ToArray();
            compileHitboxes = false;
        }
        else {
            foreach (var item in hitBoxes)
            {
                if (item.isActive(currentFrame)) {
                    foreach (var box in item.boxes)
                    {
                        box.DebugDraw(0f, Color.red);
                    }
                }
            }
        }
        if (compileHurtboxes)
        {
            if (hurtBoxes.Length != animLength)
            {
                hurtBoxes = new List<BoxRect>[animLength];
            }
            BoxCollider2D[] boxes = GetComponentsInChildren<BoxCollider2D>();
            List<List<BoxRect>> hboxes = hurtBoxes.ToList();
            foreach (BoxCollider2D b in boxes)
            {
                hboxes[currentFrame].Add(new BoxRect(b.bounds.min.x, b.bounds.min.y, b.bounds.max.x, b.bounds.max.y));
            }
            hurtBoxes = hboxes.ToArray();
            compileHurtboxes = false;
        }
        else 
        {
            if (hurtBoxes?.Length > currentFrame) {
                foreach (var hbox in hurtBoxes[currentFrame])
                {
                    hbox.DebugDraw(.1f, Color.green);
                }
                
            }
        }
        if (compileKarakey) {
            karaKeyFrames = GetComponents<KaraFrame>();
            compileKarakey = false;
        }
        if (compileBodyboxes) {
            if (bodyBox.Length!=animLength)
            {
                bodyBox = new BoxRect[animLength];
            }
            compileBodyboxes = false;
        }
        
    }
}
