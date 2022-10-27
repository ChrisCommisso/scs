using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    public class InputTracker : MonoBehaviour
    {
        public static void setFrame(int f) 
        {
            frame = f;
        }
        private static int frame;
        public static int Frame {
            get { return frame; }
        }
        public static string leftRightConversion(string index) 
        {
            switch (index)
            {
                case "4-2":
                    return "6-2";
                case "6-2":
                    return "4-2";
                case "1":
                    return "3";
                case "4":
                    return "6";
                case "7":
                    return "9";
                case "3":
                    return "1";
                case "6":
                    return "4";
                case "9":
                    return "7";
                default:
                    return index;
            }
        }
        
        public static Dictionary<string, int> player1;
        public static Dictionary<string, int> player2;
        void onInputUpdate(ControllerObject p1, ControllerObject p2) 
        {
            frame++;
            foreach (var key in player1.Keys)
            {
                player1[key] = Mathf.Clamp(player1[key] + 1, 0, 60);
                player2[key] = Mathf.Clamp(player2[key] + 1, 0, 60);
            }
            if (p1.l)
            {
                player1["l"] = 0;
            }
            if (p1.h)
            {
                player1["h"] = 0;
            }
            if (p1.down&&p1.left)
            {
                player1["1"] = 0;
            }
            if (p1.down&&!p1.left&&!p1.right)
            {
                player1["2"] = 0;
            }
            if (p1.down && p1.right)
            {
                player1["3"] = 0;
            }
            if (p1.left&&!p1.up&&!p1.down)
            {
                player1["4"] = 0;
            }
            if (!p1.right&&!p1.left && !p1.up && !p1.down)
            {
                player1["5"] = 0;
            }
            if (p1.right && !p1.up && !p1.down)
            {
                player1["6"] = 0;
            }
            if (p1.up && p1.left)
            {
                player1["7"] = 0;
            }
            if (p1.up && !p1.left &&!p1.right)
            {
                player1["8"] = 0;
            }
            if (p1.up && p1.right)
            {
                player1["9"] = 0;
            }
            if (p2.l)
            {
                player2["l"] = 0;
            }
            if (p2.h)
            {
                player2["h"] = 0;
            }
            if (p2.down && p2.left)
            {
                player2["1"] = 0;
            }
            if (p2.down && !p2.left && !p2.right)
            {
                player2["2"] = 0;
            }
            if (p2.down && p2.right)
            {
                player2["3"] = 0;
            }
            if (p2.left && !p2.up && !p2.down)
            {
                player2["4"] = 0;
            }
            if (!p2.right && !p2.left && !p2.up && !p2.down)
            {
                player2["5"] = 0;
            }
            if (p2.right && !p2.up && !p2.down)
            {
                player2["6"] = 0;
            }
            if (p2.up && p2.left)
            {
                player2["7"] = 0;
            }
            if (p2.up && !p2.left && !p2.right)
            {
                player2["8"] = 0;
            }
            if (p2.up && p2.right)
            {
                player2["9"] = 0;
            }
        }
        public void init()
        {
            player1.Add("l", 60);
            player1.Add("h", 60);
            player1.Add("1", 60);
            player1.Add("2", 60);
            player1.Add("3", 60);
            player1.Add("4", 60);
            player1.Add("5", 60);
            player1.Add("6", 60);
            player1.Add("7", 60);
            player1.Add("8", 60);
            player1.Add("9", 60);
            player1.Add("4-2", 60);
            player1.Add("6-2", 60);
            player2.Add("l", 60);
            player2.Add("h", 60);
            player2.Add("1", 60);
            player2.Add("2", 60);
            player2.Add("3", 60);
            player2.Add("4", 60);
            player2.Add("5", 60);
            player2.Add("6", 60);
            player2.Add("7", 60);
            player2.Add("8", 60);
            player2.Add("9", 60);
            player2.Add("4-2", 60);
            player2.Add("6-2", 60);
            SceneSimulator.inputTick += onInputUpdate;
        }

    }
}
