using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Character 
{
    public string name;
    public Dictionary<string, Move> Animations;

    public static Character[] loadAll() 
    {
        AnimationLoader.instance.load();
        return AnimationLoader.characters.ToArray();
    }
    public static Character loadByName(string name) {
        foreach (var character in loadAll())
        {
            if (character.name == name) 
            {
                return character;
            }
        }
        return new Character();
    }
}
public class AnimationLoader : MonoBehaviour
{
    public static AnimationLoader instance;
    public static List<Character> characters;
    private void Start()
    {
        if (instance == null)
        { instance = this;
            load();
        }

    }
    public bool load()
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Characters");
            characters = new List<Character>();
            foreach (var character_dir in dir.EnumerateDirectories())
            {
                Character temp = new Character();
                temp.Animations = new Dictionary<string, Move>();
                temp.name = character_dir.Name;
                foreach (var anim in character_dir.EnumerateDirectories())
                {
                    Move generated = new Move();
                    generated.currentFrame = 0;
                    generated.frames = Resources.LoadAll<Sprite>(anim.FullName).ToList<Sprite>();
                    generated.frameData = JsonUtility.FromJson<MoveData>(anim.FullName + "/movedata.json");
                    temp.Animations.Add(anim.Name, generated);
                }
            }
        }
        catch (Exception)
        {

            return false;
        }
        
        return true;
    }
}
