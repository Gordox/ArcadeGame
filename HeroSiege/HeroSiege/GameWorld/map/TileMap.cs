using HeroSiege.Manager;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HeroSiege.GameWorld.map
{
    class TileMap
    {
        /// <summary>
        /// Contains the background
        /// </summary>
        public List<Tile> BackGroundTexture { get; private set; }

        /// <summary>
        /// Contains the data where the units can walk (AI)
        /// </summary>
        public Tile[,] MapWakeblePath { get; private set; }

        public string MapName { get; private set; }

        public int TileSize { get; private set; }

        public TileMap(int width, int height, string mapName = null)
        {
            this.MapWakeblePath = new Tile[width, height];
            this.BackGroundTexture = new List<Tile>();

        }


        public void DrawMapTexture(SpriteBatch SB)
        {
            foreach (Tile t in BackGroundTexture)
            {
                if (t == null)
                    continue;
                t.Draw(SB);
            }
        }


        /// <summary>
        /// The width of the wakeble path.
        /// </summary>
        public int Width
        {
            get { return MapWakeblePath.GetLength(1); }
        }

        /// <summary>
        /// The height of the wakeble path.
        /// </summary>
        public int Height
        {
            get { return MapWakeblePath.GetLength(0); }
        }

        public bool GetIfWakeble(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return false;

            return MapWakeblePath[cellX, cellY].Wakeble;
        }


        public void LoadMapDataFromXMLFile(string mapName)
        {
            XDocument map = XDocument.Load(@"Content\MAPHERE");

            var allElements = (from e in map.Descendants("data")
                               select new { name = e.Parent.Attribute("name").Value, data = (e.HasElements ? "" : e.Value) }).ToList();

            for (int i = 0; i < allElements.Count; i++)
            {
                List<string> lines = allElements[i].data.Split('\n').ToList();
                CreateMapFromXmlFile(allElements[i].name, lines);
            }



            //CreateMapFromXmlFile(allElements); 0
        }
        private void CreateMapFromXmlFile(string layername, List<string> lines) //Not done
        {
            for (int y = 0; y < lines.Count; y++)
            {
                string[] lineHolder = lines[y].Split(',');

                for (int x = 0; x < lineHolder.Count(); x++)
                {

                    int id = 0;
                    try { id = Int32.Parse(lineHolder[x]); } catch { }

                    switch (layername)
                    {
                        case "Base":
                            BackGroundTexture.Add(CreteTile(layername, id, x, y));
                            break;
                        default:
                            break;
                    }

                    //Not done
                }
            }
        }


        private Tile CreteTile(string Tilename, int id, int x, int y)
        {
            return new Tile(ResourceManager.GetTexture(Tilename, id), x * TileSize + TileSize / 2,
                                                                 y * TileSize + TileSize / 2, TileSize);
        }
        private Tile CreteTile(string Tilename, int x, int y)
        {
            return new Tile(ResourceManager.GetTexture(Tilename), x * TileSize + TileSize / 2,
                                                                 y * TileSize + TileSize / 2, TileSize);
        }


    }
}
