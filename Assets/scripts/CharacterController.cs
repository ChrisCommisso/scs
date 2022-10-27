using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class CharacterController : MonoBehaviour
    {
        
        public GameObject genericCharacter;
        Move player1current;
        Move player2current;
        public Vector3 P1Velocity;
        public Vector3 P2Velocity;
        public static GameObject player1Object;
        public static Character player1Character;
        public static GameObject player2Object;
        public static Character player2Character;
        public List<string> player1Resource;
        public List<string> player2Resource;
        bool p1PrevGrounded;
        bool p2PrevGrounded;
        bool p1CurrGrounded;
        bool p2CurrGrounded;
        bool isLeft(GameObject character, bool isPlayer2) 
        {
            if (isPlayer2)
            {
                if (character.transform.position.x >= player1Object.transform.position.x)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                if (character.transform.position.x < player2Object.transform.position.x)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool tryInput(string[] input, bool isPlayer2) 
        {

            Dictionary<string, int> playerinputs = InputTracker.player1;
            GameObject playerObject = player1Object;
            if (isPlayer2) 
            {
                playerObject = player2Object;
                playerinputs = InputTracker.player2;
            }
            int inputIndex = 0;
            foreach (string buttonid in input)
            {
                string[] combinedInputs = new string[0];
                List<string> altIDs = new List<string>();
                if (buttonid.Contains("+")) 
                {
                    combinedInputs = buttonid.Split('+');
                }
                string id = buttonid;
                if (isLeft(player2Object, isPlayer2)) 
                {
                    id = InputTracker.leftRightConversion(id);
                }
                
                
                int framesSinceinput = input.Length-inputIndex*6;
                if (playerinputs[id] < framesSinceinput )
                {
                    foreach (var inputs in combinedInputs)
                    {
                        if (playerinputs[id] - playerinputs[inputs] >= 3 && playerinputs[id] - playerinputs[inputs] < 0) 
                        {
                            return false;
                        }
                    }
                    return false;
                }
                
                inputIndex++;
            }
            return true;
        }

        void onAnimate(InputTracker tracker) 
        {
            CharacterMovementComponent _p1movementcomponent = player1Object.GetComponent<CharacterMovementComponent>();
            CharacterMovementComponent _p2movementcomponent = player2Object.GetComponent<CharacterMovementComponent>();
            p1CurrGrounded = _p1movementcomponent.onGround;
            p2CurrGrounded = _p2movementcomponent.onGround;
            player1current.currentFrame++;
            if (player1current.hitstopFrames > 0)
            {
                player1current.hitstopFrames--;
            }
            else if (player1current.currentFrame == player1current.frameData.animLength)
            {
                player1current = player1Character.Animations[player1current.frameData.naturalEndAnim].Copy();
            }
            else if (player1current.frameData.landCancel && !p1PrevGrounded && p1CurrGrounded) 
            {
                player1current = player1Character.Animations["idle"].Copy();
            }
            else
            {
                foreach (string cancelData in player1current.frameData.canCancelTo)
                {
                    bool requireHit = cancelData[0] == '~';
                    string data = cancelData;
                    if (requireHit)
                    {
                        data = cancelData.Substring(1);
                    }

                    int cancelMinFrame = 0;
                    string anim = "";
                    if (data.Contains('-'))
                    {
                        string[] cData = data.Split('-');
                        anim = cData[0];
                        int.TryParse(cData[1], out cancelMinFrame);
                    }
                    if (player1current.currentFrame >= cancelMinFrame && tryInput(player1Character.Animations[anim].frameData.input, false))
                    {
                        player1current = player1Character.Animations[anim].Copy();
                        break;
                    }

                }
                player1Object.gameObject.GetComponent<SpriteRenderer>().sprite = player1current.frames[player2current.currentFrame];

            }
            player2current.currentFrame++;
            if (player2current.hitstopFrames > 0)
            {
                player2current.hitstopFrames--;
            }
            else if (player2current.currentFrame == player2current.frameData.animLength) 
            {
                player2current = player2Character.Animations[player2current.frameData.naturalEndAnim].Copy();
            }
            else if (player2current.frameData.landCancel && !p2PrevGrounded && p2CurrGrounded)
            {
                player2current = player2Character.Animations["idle"].Copy();
            }
            else
            {
                foreach (string cancelData in player2current.frameData.canCancelTo)
                {
                    bool requireHit = cancelData[0] == '~';
                    string data = cancelData;
                    if (requireHit)
                    {
                        data = cancelData.Substring(1);
                    }

                    int cancelMinFrame = 0;
                    string anim = "";
                    if (data.Contains('-'))
                    {
                        string[] cData = data.Split('-');
                        anim = cData[0];
                        int.TryParse(cData[1], out cancelMinFrame);
                    }
                    if (player2current.currentFrame>=cancelMinFrame&&tryInput(player2Character.Animations[anim].frameData.input, false)) 
                    {
                        player2current = player2Character.Animations[anim].Copy();
                        break;
                    }
                }
                player2Object.gameObject.GetComponent<SpriteRenderer>().sprite = player2current.frames[player2current.currentFrame];
            }
        }

        void onResolve() 
        {
            Move player1Next = player1current;
            Move player2Next = player2current;
            bool P1isLeft = isLeft(player1Object, false);
            #region resolve p1&p2 hit
            Vector3 player1Position = player1Object.transform.position;
            Vector3 player2Position = player2Object.transform.position;
            BoxRect[] player1Hurtboxes = player1current.frameData.hurtBoxes[player1current.currentFrame].ToArray();
            BoxRect[] player2Hurtboxes = player2current.frameData.hurtBoxes[player1current.currentFrame].ToArray();
            List<HitboxGroup> player1HitboxesForFrame = new List<HitboxGroup>();
            List<HitboxGroup> player2HitBoxesForFrame = new List<HitboxGroup>();
            for (int i = 0; i < player1Hurtboxes.Length; i++)
            {//scale and reflect boxes

                player1Hurtboxes[i] = new BoxRect(((player1Hurtboxes[i].X) * player1Object.transform.localScale.x), ((player1Hurtboxes[i].Y) * player1Object.transform.localScale.y),((player1Hurtboxes[i].Width) * player1Object.transform.localScale.x), ((player1Hurtboxes[i].Height) * player1Object.transform.localScale.y));
                if (P1isLeft) 
                {
                    player1Hurtboxes[i].X *= -1;
                }
                player1Hurtboxes[i].X += player1Object.transform.position.x;
                player1Hurtboxes[i].Y += player1Object.transform.position.y;
            }
            for (int i = 0; i < player2Hurtboxes.Length; i++)
            {
                player2Hurtboxes[i] = new BoxRect(((player2Hurtboxes[i].X)* player2Object.transform.localScale.x), ((player2Hurtboxes[i].Y) * player2Object.transform.localScale.y),((player2Hurtboxes[i].Width) * player2Object.transform.localScale.x), ((player2Hurtboxes[i].Height) * player2Object.transform.localScale.y));
                if (!P1isLeft)
                {
                    player2Hurtboxes[i].X *= -1;
                }
                player2Hurtboxes[i].X += player2Object.transform.position.x;
                player2Hurtboxes[i].Y += player2Object.transform.position.y;
            }
            
            foreach (var hitboxGroup in player1current.frameData.hitBoxes)
            {
                if (player1current.currentFrame >= hitboxGroup.activeOn && player1current.currentFrame < hitboxGroup.activeOn + hitboxGroup.activeFor) 
                {
                    if (P1isLeft) 
                    {
                        player1HitboxesForFrame.Add(hitboxGroup + player1Object.transform.position);
                    }
                    else
                    {
                        player1HitboxesForFrame.Add(player1Object.transform.position-hitboxGroup);
                    }
                }
            }
            foreach (var hitboxGroup in player2current.frameData.hitBoxes)
            {
                if (player2current.currentFrame >= hitboxGroup.activeOn && player2current.currentFrame < hitboxGroup.activeOn + hitboxGroup.activeFor)
                {
                    player2HitBoxesForFrame.Add(hitboxGroup + player2Object.transform.position);
                }
            }
            
            
            foreach (var hitboxGroup in player1HitboxesForFrame)
            {

                foreach (var hitbox in hitboxGroup.boxes)
                {
                    foreach (var hurtbox in player2Hurtboxes)
                    {
                        if (hitbox.IntersectsWith(hurtbox))
                        {
                            if (player2current.currentFrame >= player2current.frameData.hitInvulStartup && player2current.currentFrame < player2current.frameData.hitInvulEnd)
                            {
                                if (hitboxGroup.blockType != defenseType._throw)
                                {
                                    break;
                                }

                            }
                            if (player2current.currentFrame >= player2current.frameData.armorStartup && player2current.currentFrame < player2current.frameData.armorEnd)
                            {
                                //take damage
                            }
                            if (player2current.currentFrame >= player2current.frameData.throwInvulStartup && player2current.currentFrame < player2current.frameData.throwInvulEnd)
                            {
                                if (hitboxGroup.blockType == defenseType._throw)
                                {
                                    break;
                                }
                            }
                         
                            player2Next = player2Character.Animations[hitboxGroup.onHitAnim].Copy();
                            player2Next.frameData.animLength = hitboxGroup.hitstun;
                            player2Next.loop = true;
                            player2Next.frameData.karaKeyFrames = hitboxGroup.velocites;
                            player2Next.hitstopFrames = hitboxGroup.onHitEnemyHitStop;
                            player1current.hitstopFrames = hitboxGroup.onHitFriendlyHitStop;
                        }
                    }
                }
            }
            foreach (var hitboxGroup in player2HitBoxesForFrame)
            {
                foreach (var hitbox in hitboxGroup.boxes)
                {
                    foreach (var hurtbox in player1Hurtboxes)
                    {
                        if (hitbox.IntersectsWith(hurtbox))
                        {
                            if (player1current.currentFrame >= player1current.frameData.hitInvulStartup && player1current.currentFrame < player1current.frameData.hitInvulEnd)
                            {
                                if (hitboxGroup.blockType != defenseType._throw) 
                                {
                                    break;
                                }
                                
                            }
                            if (player1current.currentFrame >= player1current.frameData.armorStartup && player1current.currentFrame < player1current.frameData.armorEnd)
                            {
                                //take damage
                                //hitstop
                                //flash
                                break;
                            }
                            if (player1current.currentFrame >= player1current.frameData.throwInvulStartup && player1current.currentFrame < player1current.frameData.throwInvulEnd) 
                            {
                                if (hitboxGroup.blockType == defenseType._throw)
                                {
                                    break;
                                }
                            }

                            player1Next = player1Character.Animations[hitboxGroup.onHitAnim].Copy();
                            player1Next.frameData.animLength = hitboxGroup.hitstun;
                            player1Next.loop = true;
                            player1Next.frameData.karaKeyFrames = hitboxGroup.velocites;
                            player1Next.hitstopFrames = hitboxGroup.onHitEnemyHitStop;
                            player2current.hitstopFrames = hitboxGroup.onHitFriendlyHitStop;
                            
                        }
                    }
                }
            }
            #endregion
            player1current = player1Next;//set appropriate animation
            player2current = player2Next;//set appropriate animation
            player1Object.GetComponent<SpriteRenderer>().sprite = player1current.frames[player1current.currentFrame];//set frame visually
            player2Object.GetComponent<SpriteRenderer>().sprite = player2current.frames[player2current.currentFrame];//set frame visually
            player1Object.GetComponent<CharacterMovementComponent>().setDimensions(player1current.frameData.bodyBox[player1current.currentFrame].Width, player1current.frameData.bodyBox[player1current.currentFrame].Height, player1current.frameData.bodyBox[player1current.currentFrame].X, player1current.frameData.bodyBox[player1current.currentFrame].Y);//set frame visually
            player2Object.GetComponent<CharacterMovementComponent>().setDimensions(player2current.frameData.bodyBox[player2current.currentFrame].Width, player2current.frameData.bodyBox[player2current.currentFrame].Height, player2current.frameData.bodyBox[player2current.currentFrame].X, player2current.frameData.bodyBox[player2current.currentFrame].Y);//set frame visually
            p1PrevGrounded = p1CurrGrounded;
            p2PrevGrounded = p2CurrGrounded;
            //actions include
            //spawn animation as object(for projectiles)
            //set opponent position relative to character
            //set opponent position relative to screen
            //set opponent position relative to 

        }

        Vector3 ApplyGravity(Vector3 v,bool isPlayer2) 
        {
            if(isPlayer2)
            return v + new Vector3(0, -player2current.frameData.gravityMult[player2current.currentFrame], 0);
            return v + new Vector3(0, -player1current.frameData.gravityMult[player1current.currentFrame], 0);
        }

        void onPhysics() 
        {
            if (P1Velocity == null)
                P1Velocity = Vector3.zero;
            if (P2Velocity == null)
                P2Velocity = Vector3.zero;
            
            foreach (var karaFrame in player1current.frameData.karaKeyFrames)
            {
                if (karaFrame.frame == player1current.currentFrame || karaFrame.frame == -1) 
                {
                    P1Velocity = karaFrame.velocity;
                    break;
                }
            }
           
            P1Velocity = ApplyGravity(P1Velocity, false);
   
            foreach (var karaFrame in player2current.frameData.karaKeyFrames)
            {
                if (karaFrame.frame == player2current.currentFrame || karaFrame.frame == -1)
                {
                    P2Velocity = karaFrame.velocity;
                    break;
                }
            }
            
            P2Velocity = ApplyGravity(P2Velocity, true);

            player1Object.GetComponent<CharacterMovementComponent>().DoMovement(P1Velocity);
            player2Object.GetComponent<CharacterMovementComponent>().DoMovement(P2Velocity);
        }

        public void init()
        {
            SceneSimulator.physicsTick += onPhysics;
            SceneSimulator.animationTick += onAnimate;
            SceneSimulator.resolutionTick += onResolve;
            player1Object = Instantiate(genericCharacter);
            player2Object = Instantiate(genericCharacter);
            player1Character = Character.loadByName("Bart");
            player2Character = Character.loadByName("Bart");
            player1current = player1Character.Animations["idle"].Copy();
            player2current = player2Character.Animations["idle"].Copy();

        }
    }
}
