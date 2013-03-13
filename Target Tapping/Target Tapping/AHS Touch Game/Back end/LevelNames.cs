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
    class LevelNames
    {
        [Serializable]
        private struct SaveLevelNames
        {
            public List<string> filename;
        }

        public static List<string> filename { get; set; }
        private static StorageDevice device;
        private static IAsyncResult result;

        public LevelNames()
        {
            filename = loadLevelName();
        }

        public void addFilename(string newFilename)
        {
            filename.Add(newFilename);
            saveLevelName(filename);
        }

        public void removeFilename(string oldFilename)
        {
            int position = 0;
            foreach (var file in filename)
            {
                if (oldFilename == file)
                {
                    filename.RemoveAt(position);
                }
                else
                {
                    Console.WriteLine("In LevelNames.cs could not remove file. File {0} does not exist", oldFilename);
                }
                position = position + 1;
            }
            saveLevelName(filename);
        }

        public void changeFilename(string oldFilename, string newFilename)
        {
            int position = 0;
            foreach (var file in filename)
            {
                if (oldFilename == file)
                {
                    filename[position] = newFilename;
                }
                position = position + 1;
            }
        }

        private static List<string> loadLevelName()
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();
            if (container.FileExists("ListofFilenames.sav"))
            {
                Stream stream = container.OpenFile("ListofFilenames.save", FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelNames));
                SaveLevelNames data = (SaveLevelNames)serializer.Deserialize(stream);
                filename = data.filename;
                stream.Close();
                container.Dispose();
            }
            else
            {
                filename = new List<string>();
            }
            return filename;
        }

        private static void saveLevelName(List<string> filename)
        {
            SaveLevelNames data = new SaveLevelNames();
            data.filename = filename;

            IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            if (container.FileExists("ListofFilenames.sav"))
                container.DeleteFile("ListofFilenames.sav");
            Stream stream = container.CreateFile("ListofFilenames.sav");
            XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelNames));
            serializer.Serialize(stream, data);
            stream.Close();
            container.Dispose();
            result.AsyncWaitHandle.Close();
        }
    }
}
