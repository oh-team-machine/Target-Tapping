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
    class LevelLoad
    {
        [Serializable]
        private struct SaveLevelData
        {
            public List<List<TargetTapping.Back_end.Object>> objectList;
            public int currentPosition;
            public bool multiSelect;
            public int upTime;
            public int holdTime;
            public string filename;
        }

        private static StorageDevice device;
        private static IAsyncResult result;

        public static Level loadLevel(string filename)
        {
            Level level = new Level();
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
                level.objectList = data.objectList;
                level.currentPosition = data.currentPosition;
                level.multiSelect = data.multiSelect;
                level.holdTime = data.holdTime;
                level.upTime = data.upTime;
                level.levelName = data.filename;
                stream.Close();
                container.Dispose();
            }
            else
            {
                Console.WriteLine("No such filename");
            }
            return level;
        }
    }
}
