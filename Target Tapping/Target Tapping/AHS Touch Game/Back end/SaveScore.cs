using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Specialized;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace TargetTapping.Back_end
{
    class SaveScore
    {
        [Serializable]
        public struct SaveLevelNames
        {
            public List<string> levelName;
            public List<int> timeScore;
            public List<int> hitsScore;
        }

        public static List<string> levelNames;
        public static List<int> timeScores;
        public static List<int> hitsScores;

        private static StorageDevice device;

       public SaveScore()
        {
            device = null;
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.loadLevelName, null);
        }

        void loadLevelName(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
            StorageContainer container = device.EndOpenContainer(r);
            if (container.FileExists("ListofScores.sav"))
            {
                Stream stream = container.OpenFile("ListofScores.sav", FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelNames));
                stream.Seek(0, SeekOrigin.Begin);
                SaveLevelNames data = (SaveLevelNames)serializer.Deserialize(stream);
                levelNames = data.levelName;
                timeScores = data.timeScore;
                hitsScores = data.hitsScore;
                stream.Close();
                container.Dispose();
            }
        }

        void save(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                SaveLevelNames data = new SaveLevelNames();
                data.levelName = levelNames;
                data.timeScore = timeScores;
                data.hitsScore = hitsScores;

                IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists("ListofScores.sav"))
                    container.DeleteFile("ListofScores.sav");
                Stream stream = container.CreateFile("ListofScores.sav");
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelNames));
                serializer.Serialize(stream, data);
                stream.Close();
                container.Dispose();
            }
        }

        public void saveScore(string newLevelName, int newTimeScore, int newHitsScore)
        {
            levelNames.Add(newLevelName);
            timeScores.Add(newTimeScore);
            hitsScores.Add(newHitsScore);
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.save, null);
        }
    }
}
