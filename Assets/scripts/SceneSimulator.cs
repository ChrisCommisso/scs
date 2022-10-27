using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    
    public delegate void InputPollTick(ControllerObject p1, ControllerObject p2);
    public delegate void AnimationTick(InputTracker input);
    public delegate void AnimationResolutionTick();
    public delegate void PhysicsTick();
    [Serializable]
    class SceneSimulator : MonoBehaviour
    {
        public static ControllerObject p1mostrecent;
        public static ControllerObject p2mostrecent;
        public static CharacterController characterControllerInstance;
        public static InputTracker inputTrackerInstance;
        int frameskipcounter = 0;
        public static InputPollTick inputTick;
        public static PhysicsTick physicsUpdates;
        public static PhysicsTick physicsTick;
        public static AnimationTick animationTick;
        public static AnimationResolutionTick resolutionTick;
        void gameTick() 
        {
            animationTick(inputTrackerInstance);
            resolutionTick.Invoke();
            frameskipcounter++;
            if (frameskipcounter >= 5)
            {
                animationTick.Invoke(inputTrackerInstance);
                resolutionTick.Invoke();
                frameskipcounter = 0;
            }
        }
        IEnumerator updateTick() 
        {
            int i = 0;
            while (true)
            {
                //read input here
                inputTick.Invoke(p1mostrecent,p2mostrecent);//update input tracker
                if (i >= 4)//15(4/60) is our framerate after frameskip
                {
                    gameTick();
                    i = 0;
                }
                physicsTick.Invoke();
                i++;
                yield return new WaitForSecondsRealtime(1f / 60f);//physics and input update like this
            }
        }
        void Start()
        {
            characterControllerInstance = new CharacterController();
            characterControllerInstance.init();
            inputTrackerInstance = new InputTracker();
            inputTrackerInstance.init();
            StartCoroutine(updateTick());
        }
       
    }
}
