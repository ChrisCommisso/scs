using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public enum defenseType 
    {
    _high,
    _low,
    _unblockable,
    _throw
    }
    [Serializable]
    public class HitboxGroup
    {
        public defenseType blockType; 
        public int activeOn;
        public int activeFor;
        public KaraFrame[] velocites;
        public List<BoxRect> boxes;
        public int hitstun;
        public int blockstun;
        public string onHitAnim;
        public int onHitEnemyHitStop;
        public int onHitFriendlyHitStop;
        public bool isActive(int curframe) {
            return (curframe <= activeOn + activeFor && curframe >= activeOn);
        }
        public static HitboxGroup operator +(HitboxGroup a, Vector3 position)
        {
            HitboxGroup b = new HitboxGroup();
            b.blockType = a.blockType;
            b.velocites = a.velocites;
            b.onHitAnim = a.onHitAnim;
            b.onHitEnemyHitStop = a.onHitEnemyHitStop;
            b.onHitFriendlyHitStop = a.onHitFriendlyHitStop;
            b.hitstun = a.hitstun;
            b.activeOn = a.activeOn;
            b.activeFor = a.activeFor;
            b.boxes = new List<BoxRect>();
            foreach (var item in a.boxes)
            {
                b.boxes.Add(new BoxRect(item.X + position.x, item.Y + position.y, item.Width, item.Height));
            }
            return b;
        }
        public static HitboxGroup operator -(Vector3 position, HitboxGroup a)
        {
            HitboxGroup b = new HitboxGroup();
            b.blockType = a.blockType;
            b.velocites = a.velocites;
            b.onHitAnim = a.onHitAnim;
            b.onHitEnemyHitStop = a.onHitEnemyHitStop;
            b.onHitFriendlyHitStop = a.onHitFriendlyHitStop;
            b.hitstun = a.hitstun;
            b.activeOn = a.activeOn;
            b.activeFor = a.activeFor;
            foreach (var item in a.boxes)
            {
                b.boxes.Add(new BoxRect(position.x-item.X, item.Y+position.y, item.Width, item.Height));
            }
            return b;
        }

    }
}
