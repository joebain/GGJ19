using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Rule
{
    public MapGenerator.PipeSectionTypes From;
    public MapGenerator.PipeSectionTypes[] To;
}

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public int SectionCount = 100;
    public int SectionLength = 8;

    public TileBase TopPipeWall, BottomPipeWall, Water, Earth;

    public Rule[] Rules;
    Dictionary<PipeSectionTypes, Rule> ruleDictionary;

    void Start()
    {
        DoGenerateMap();
    }

    public enum PipeSectionTypes
    {
        Small,
        Medium,
        Big,
        Split,
        SmallGoingUp,
        SmallGoingDown,
        MediumGoingUp,
        MediumGoingDown,
        Transition
    }

    List<PipeSectionTypes> pipeSections = new List<PipeSectionTypes>();

    [InspectorButton("DoGenerateMap")]
    public bool _doGenerateMap;

    public void DoGenerateMap()
    {
        GenerateMap(UnityEngine.Random.Range(1, 100000));
    }
    /// <summary>
    /// When generating a map we want it to mainly go left to right, we want to vary the width between narrow, medium, and wide, we want to
    /// have some sloped sections, and the camera will track up and down accordingly. For now we won't worry about vertical sections or right to
    /// left sections.
    /// 
    /// It would be nice to be able to introduce feature sections, such as split tunnels which rejoin, gaps in the pipe, that you have to jump,
    /// waterfalls maybe, cross sections which might have enemies or currents crossing vertically.
    /// 
    /// We then need another layer which will place enemies, and another layer which will place scenery like plants, rocks, etc. Some enemies or
    /// obstacles could require changes to the background, very large enemies coming out of a side pipe, or plants bursting through a crack
    /// in the pipe.
    /// 
    /// It would be nice to have another layer for parallax objects in the background and foreground.
    /// 
    /// Probably another layer for currents in the pipe too, which might not be visible in the typical way, but would spawn some particle effect
    /// or mean some certain animation is played on the background tiles in that spot.
    /// </summary>
    /// <param name="seed"></param>
    public void GenerateMap(int seed)
    {
        FillRuleDictionary();

        tilemap.ClearAllTiles();
        GenerateSections(seed);
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        Vector3Int centre = new Vector3Int(0, 0, 0);

        int t = 0;
        PipeSectionTypes section = pipeSections[0], prevSection = pipeSections[0], nextSection = pipeSections[1];
        for (int s = 0; s < pipeSections.Count - 1; s++)
        {
            prevSection = section;
            section = pipeSections[s];
            nextSection = pipeSections[s + 1];
            int tMax = t + SectionLength;
            for (; t < tMax; t++)
            {
                int width = GetSectionWidth(section, prevSection, nextSection, t);
                tilemap.SetTile(centre + Vector3Int.up * width, TopPipeWall);
                tilemap.SetTile(centre + Vector3Int.down * width, BottomPipeWall);
                centre.x++;
            }
        }
    }

    private void GenerateSections(int seed)
    {
        var rng = new System.Random(seed);
        pipeSections = new List<PipeSectionTypes>(SectionCount);
        int sectionMax = ((int)PipeSectionTypes.Transition) - 1;
        PipeSectionTypes currentSection = PipeSectionTypes.Medium, nextSection;
        for (int s = 0; s < SectionCount; s++)
        {
            Rule rule = ruleDictionary[currentSection];
            nextSection = rule.To[rng.Next(rule.To.Length)];
            pipeSections.Add(nextSection);
            if (s > 0 && pipeSections[s - 1] != pipeSections[s] && pipeSections[s - 1] != PipeSectionTypes.Transition)
            {
                if (s >= SectionCount - 1)
                {
                    pipeSections[s] = pipeSections[s - 1];
                    break;
                }
                pipeSections.Add(pipeSections[s]);
                pipeSections[s] = PipeSectionTypes.Transition;
                s++;
            }
        }
    }

    private void FillRuleDictionary()
    {
        ruleDictionary = new Dictionary<PipeSectionTypes, Rule>();
        for (int r = 0; r < Rules.Length; r++)
        {
            ruleDictionary[Rules[r].From] = Rules[r];
        }
    }

    int GetSectionWidth(PipeSectionTypes section, PipeSectionTypes prevSection = PipeSectionTypes.Medium, PipeSectionTypes nextSection = PipeSectionTypes.Medium, int t = 0) {
        switch (section) {
            case PipeSectionTypes.Small:
            case PipeSectionTypes.SmallGoingDown:
            case PipeSectionTypes.SmallGoingUp:
                return 8;
            case PipeSectionTypes.Medium:
            case PipeSectionTypes.MediumGoingDown:
            case PipeSectionTypes.MediumGoingUp:
                return 16;
            case PipeSectionTypes.Big:
                return 24;
            case PipeSectionTypes.Transition:
                return GetTransitionSectionWidth(prevSection, nextSection, t);
            default:
                return 16;
        }
    }

    private int GetTransitionSectionWidth(PipeSectionTypes prevSection, PipeSectionTypes nextSection, int t)
    {
        int fromWidth = GetSectionWidth(prevSection);
        int toWidth = GetSectionWidth(nextSection);
        float theta = (float)(t%SectionLength) / SectionLength;
        return Mathf.FloorToInt(Mathf.Lerp(fromWidth, toWidth, theta));
    }
}
