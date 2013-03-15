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
    public class SaveLevel
    {
        [Serializable]
        public struct SaveLevelData
        {
            public List<List<TargetTapping.Back_end.Object>> objectList;
            public int currentPosition;
            public bool multiSelect;
            public int upTime;
            public int holdTime;
            public string filename;
        }

        private static StorageDevice device;
        public Level level;

        public void initiateSave(Level levelPassed) {
            level = levelPassed;
            device = null;
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.saveLevel, null);
        }

        void saveLevel(IAsyncResult result)
        {
            SaveLevelData data = new SaveLevelData();
            data.objectList = level.objectList;
            data.currentPosition = level.currentPosition;
            data.multiSelect = level.multiSelect;
            data.upTime = level.upTime;
            data.holdTime = level.holdTime;
            data.filename = level.levelName;

            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists(level.levelName))
                    container.DeleteFile(level.levelName);
                Stream stream = container.CreateFile(level.levelName);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelData));
                serializer.Serialize(stream, data);
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }
    }
}
