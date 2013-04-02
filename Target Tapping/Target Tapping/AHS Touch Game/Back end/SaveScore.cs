using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace TargetTapping.Back_end
{
    public class SaveScore
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

        private static bool check = true;
        private static StorageDevice device;

       public SaveScore()
       {
           device = null;
           if (check) {
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.loadLevelName, null);
           }
           if (check == false)
           {
                levelNames = new List<string>();
                timeScores = new List<int>();
                hitsScores = new List<int>();
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.save, null);
           }
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
            else
            {
                check = false;
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
