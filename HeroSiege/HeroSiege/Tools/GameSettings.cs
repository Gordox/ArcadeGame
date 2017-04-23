using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroSiege.Tools
{
    public enum CharacterType
    {
        ElvenArcher,
        Mage,
        Gryphon_Rider,
        FootMan,
        Dwarven,
        Gnomish_Flying_Machine,
        Knight,
        None
    }

    public enum Difficult
    {
        Easy,
        Normal,
        Hard,
        Extream,
        Insane,
        Hell
    }

    public enum GameMode
    {
        singlePlayer,
        Multiplayer
    }

    public class GameSettings
    {
        public CharacterType playerOne { get; set; }
        public CharacterType playerTwo { get; set; }
        public Difficult GameDifficulty { get; set; }
        public GameMode GameMode { get; set; }

        public string MapName { get; set; }

        public float MasterVolym { get; set; }

        public GameSettings()
        {
            GameMode = GameMode.singlePlayer;
            playerOne = CharacterType.None;
            playerTwo = CharacterType.None;
            MasterVolym = 0.55f;

        }


    }
}
