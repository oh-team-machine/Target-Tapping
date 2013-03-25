using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace TargetTapping.Back_end
{
    public class LevelLoad
    {
        [Serializable]
        public struct SaveLevelData
        {
            public List<List<SerializableEntity>> objectList;
            public int currentPosition;
            public bool multiSelect;
            public int upTime;
            public int holdTime;
            public string filename;
        }

        private static StorageDevice device;
        private static SerializableLevel level;
        private static string filename;
        public SerializableLevel initiateLoad(string filenamePassed)
        {
            level = new SerializableLevel(new Level());
            filename = filenamePassed + ".sav";
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.loadLevel, null);
            return level;
        }

        void loadLevel(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();
            if (container.FileExists(filename))
            {
                Stream stream = container.OpenFile(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelData));
                SaveLevelData data = (SaveLevelData)serializer.Deserialize(stream);
                level.entityList = data.objectList;
                level.currentPosition = data.currentPosition;
                level.multiSelect = data.multiSelect;
                level.holdTime = data.holdTime;
                level.upTime = data.upTime;
                level.levelName = data.filename;
                stream.Close();
                container.Dispose();
            }
        }
    }
}
