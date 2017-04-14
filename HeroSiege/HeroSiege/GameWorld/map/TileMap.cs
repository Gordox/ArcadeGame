using HeroSiege.Manager;
using Microsoft.Xna.Framework;
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

        /// <summary>
        /// Fog of war
        /// </summary>
        public Tile[,] FogOfWar { get; private set; }

        /// <summary>
        /// Contains the bounderies were the player cant walk cross
        /// </summary>
        public List<Rectangle> Hitboxes { get; private set; }

        //--- Entitis Spawn ---//
        public Vector2 PlayerOneSpawn { get; private set; }
        public Vector2 PlayerTwoSpawn { get; private set; }

        //--- Buildings ---//
        public List<Vector2> EnemieTowerPos { get; private set; }
        public List<Vector2> EnemieSpawnerPos { get; private set; }

        public List<Vector2> HeroTowerPos { get; private set; }
        public Vector2 HeroCastle { get; private set; }

        public List<Vector2> HeroBalista { get; private set; }

        public List<Vector2> Portal { get; private set; }



        public string MapName { get; private set; }

        public int TileSize { get; private set; }
        public int FogOfWarTileSize { get; private set; }

        //----- Constructor -----//
        public TileMap(string mapName = null)
        {
            TileSize = 32;
            FogOfWarTileSize = 64;
            LoadMapDataFromXMLFile(mapName);
        }

        //----- Draw Map, fogofwar, wakeble tiles -----//
        public void DrawMapTexture(SpriteBatch SB)
        {
            foreach (Tile t in BackGroundTexture)
            {
                if (t == null)
                    continue;
                t.Draw(SB);
            }
        }

        public void DrawFogOfWar(SpriteBatch SB)
        {
            for (int y = 0; y < FogOfWar.GetLength(0); y++)
            {
                for (int x = 0; x < FogOfWar.GetLength(1); x++)
                {
                    if (FogOfWar[y, x] == null)
                        continue;

                    if (FogOfWar[x, y].Visibility == FogOfWarState.unexplored || 
                        FogOfWar[x, y].Visibility == FogOfWarState.explored)
                        FogOfWar[x, y].Draw(SB);
                }
            }
        }

        public void DrawWakebleTiles(SpriteBatch SB)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (MapWakeblePath[x, y] != null)
                        MapWakeblePath[x, y].Draw(SB);
                }
            }
        }

        //----- Initiator and Loadings -----//
        public void InitMap(int width, int height)
        {
            this.BackGroundTexture = new List<Tile>();
            this.MapWakeblePath = new Tile[width, height];
            this.FogOfWar = new Tile[width, height];
            this.Hitboxes = new List<Rectangle>();
            this.HeroTowerPos = new List<Vector2>();
            this.EnemieTowerPos = new List<Vector2>();
            this.EnemieSpawnerPos = new List<Vector2>();
            this.Portal = new List<Vector2>();
            this.HeroBalista = new List<Vector2>();
        }

        public void LoadMapDataFromXMLFile(string mapName)
        {
            XDocument map = XDocument.Load(@"Content\Assets\Maps\Map_2.0.xml");

            var mapSize = (from e in map.Descendants("map")
                           select new { Width = e.Attribute("width").Value, Height = e.Attribute("height").Value }).ToList();

            var allTileLayers = (from e in map.Descendants("data")
                               select new { name = e.Parent.Attribute("name").Value, data = (e.HasElements ? "" : e.Value) }).ToList();

            var allObjects = (from e in map.Descendants("object")
                               select new { name = e.Parent.Attribute("name").Value,
                                   X = e.Attribute("x").Value,
                                   Y = e.Attribute("y").Value,
                                   Width = e.Attribute("width").Value,
                                   Height = e.Attribute("height").Value}).ToList();


            InitMap(Int32.Parse(mapSize[0].Width), Int32.Parse(mapSize[0].Height));

            for (int i = 0; i < allTileLayers.Count; i++)
            {
                List<string> lines = allTileLayers[i].data.Split('\n').ToList();
                lines.RemoveAt(0);
                lines.RemoveAt(lines.Count - 1);
                CreateMapFromXmlFile(allTileLayers[i].name, lines);
            }


            for (int i = 0; i < allObjects.Count; i++)
            {
                int xPos = Int32.Parse(allObjects[i].X) + TileSize / 2;
                int yPos = Int32.Parse(allObjects[i].Y) + TileSize / 2;

                if (allObjects[i].name.Equals("HitBoxes"))
                    Hitboxes.Add(new Rectangle(xPos, Int32.Parse(allObjects[i].Y), //1.5 is a value to make it work
                                                     Int32.Parse(allObjects[i].Width),
                                                     Int32.Parse(allObjects[i].Height)));


                if (allObjects[i].name.Equals("HeroTower"))
                    HeroTowerPos.Add(new Vector2(xPos, yPos));
                if (allObjects[i].name.Equals("HeroCastle"))
                    HeroCastle = (new Vector2(xPos, yPos));
                if (allObjects[i].name.Equals("EnemieTower"))
                    EnemieTowerPos.Add(new Vector2(xPos, yPos));
                if (allObjects[i].name.Equals("EnemieSpawner"))
                    EnemieSpawnerPos.Add(new Vector2(xPos, yPos));
                if (allObjects[i].name.Equals("Teleport"))
                    Portal.Add(new Vector2(xPos, yPos));
                if (allObjects[i].name.Equals("HeroBalista"))
                    HeroBalista.Add(new Vector2(xPos, yPos));
            }
        }

        //----- Create Map, fogofwar, wakeble tiles -----//
        private void CreateMapFromXmlFile(string layername, List<string> lines) //Not done
        {
            for (int y = 0; y < lines.Count; y++)
            {
                string[] lineHolder = lines[y].Split(',');

                for (int x = 0; x < lineHolder.Count(); x++)
                {
                    if (lineHolder[x] == "-1")
                        continue;

                    int id = 0;
                    try { id = Int32.Parse(lineHolder[x]); } catch { }

                    switch (layername)
                    {
                        case "BaseLayer":
                            BackGroundTexture.Add(CreateTile(layername, id, x, y));
                            break;

                        case "WalkebleLayer":
                            MapWakeblePath[x, y] = CreateWalkebleTile(layername, id, x, y);
                            break;

                        case "SomethingHere":

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void CreateFogOfWar()
        {
            for (int y = 0; y < FogOfWar.GetLength(0); y++)
            {
                for (int x = 0; x < FogOfWar.GetLength(1); x++)
                {
                    FogOfWar[x, y] = CreateTile("FogOfWar",x,y); //Fix later
                }
            }
        }


        //----- Create Tile functions -----//
        /// <summary>
        /// Create a background tile
        /// </summary>
        /// <param name="Tilename"></param>
        /// <param name="id"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Tile CreateTile(string Tilename, int id, int x, int y)
        {
            return new Tile(ResourceManager.GetTexture(Tilename, id), x * TileSize + TileSize / 2,
                                                                 y * TileSize + TileSize / 2, TileSize);
        }
        /// <summary>
        /// Creates a wakeble and non-wakeble tile
        /// </summary>
        /// <param name="Tilename"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isWakeble"></param>
        /// <returns></returns>
        private Tile CreateWalkebleTile(string layerName, int id, int x, int y)
        {
            if (id == 0)
                return new Tile(ResourceManager.GetTexture(layerName, id), x * TileSize + TileSize / 2,
                                                                           y * TileSize + TileSize / 2, TileSize, WalkTypes.Walkable);
            else if (id == 1)
                return new Tile(ResourceManager.GetTexture(layerName, id), x * TileSize + TileSize / 2,
                                                                           y * TileSize + TileSize / 2, TileSize, WalkTypes.Grass);
            else if (id == 2)
                return new Tile(ResourceManager.GetTexture(layerName, id), x * TileSize + TileSize / 2,
                                                                           y * TileSize + TileSize / 2, TileSize, WalkTypes.Obstacle, false);
            else
                return null;
        }
        /// <summary>
        /// Create a fog of war tile
        /// </summary>
        /// <param name="Tilename"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Tile CreateTile(string Tilename, int x, int y)
        {
            return new Tile(ResourceManager.GetTexture(Tilename), x * TileSize + TileSize / 2,
                                                                 y * TileSize + TileSize / 2, TileSize, FogOfWarState.unexplored);
        }


        //----- Geter or returners -----//
        /// <summary>
        /// The width of the wakeble path.
        /// </summary>
        public int Width
        {
            get { return MapWakeblePath.GetLength(0); }
        }

        /// <summary>
        /// The height of the wakeble path.
        /// </summary>
        public int Height
        {
            get { return MapWakeblePath.GetLength(1); }
        }
        
        public bool GetIfWakeble(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return false;

            if (MapWakeblePath[cellX, cellY] == null)
                return false;

            return MapWakeblePath[cellX, cellY].Wakeble;
        }

        public WalkTypes GetTileType(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return WalkTypes.None;

            if (MapWakeblePath[cellX, cellY] == null)
                return WalkTypes.None;

            return MapWakeblePath[cellX, cellY].WalkType;

        }
    }
}
