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
    class SaveLevel
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

        public static void SaveLevel(Level levelPassed)
        {
            SaveLevelData data = new SaveLevelData();
            data.objectList = levelPassed.objectList;
            data.currentPosition = levelPassed.currentPosition;
            data.multiSelect = levelPassed.multiSelect;
            data.upTime = levelPassed.upTime;
            data.holdTime = levelPassed.holdTime;
            data.filename = levelPassed.levelName;

            IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            if (container.FileExists(levelPassed.levelName))
                container.DeleteFile(levelPassed.levelName);
            Stream stream = container.CreateFile(levelPassed.levelName);
            XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelData));
            serializer.Serialize(stream, data);
            stream.Close();
            container.Dispose();
            result.AsyncWaitHandle.Close();
        }
    }
}
