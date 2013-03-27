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
    public class LevelNames
    {
        [Serializable]
        public struct SaveLevelNames
        {
            public List<string> filename;
        }

        public static List<string> filenames;
        private static StorageDevice device;

        private static bool check = true;

        public LevelNames()
        {
            device = null;
            if(check)
            {
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.loadLevelName, null);
            }
            if(check == false)
            {
                filenames = new List<string>();
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.saveLevelName, null);
            }
        }

        public List<string> getFileNames() 
        {
            return filenames;
        }

        void loadLevelName(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
            StorageContainer container = device.EndOpenContainer(r);
            if (container.FileExists("ListofFilenames.sav"))
            {
                Stream stream = container.OpenFile("ListofFilenames.sav", FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelNames));
                stream.Seek(0, SeekOrigin.Begin);
                SaveLevelNames data = (SaveLevelNames)serializer.Deserialize(stream);
                filenames = data.filename;
                stream.Close();
                container.Dispose();
            }
            else
            {
                check = false;
            }
        }

        void saveLevelName(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                SaveLevelNames data = new SaveLevelNames();
                data.filename = filenames;

                IAsyncResult r = device.BeginOpenContainer("MyGamesStorage", null, null);
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists("ListofFilenames.sav"))
                    container.DeleteFile("ListofFilenames.sav");
                Stream stream = container.CreateFile("ListofFilenames.sav");
                XmlSerializer serializer = new XmlSerializer(typeof(SaveLevelNames));
                serializer.Serialize(stream, data);
                stream.Close();
                container.Dispose();
            }
        }

        public void addFilename(string newFilename)
        {
            if (filenames.Count != 0)
            {
                bool match = true;
                foreach (var filename in filenames)
                {
                    if (newFilename == filename)
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    filenames.Add(newFilename);
                    StorageDevice.BeginShowSelector(PlayerIndex.One, this.saveLevelName, null);
                }
            }
            else
            {
                filenames.Add(newFilename);
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.saveLevelName, null);
            }
        }

        public void removeFilename(string oldFilename)
        {
            int position = 0;
            foreach (var file in filenames)
            {
                if (oldFilename == file)
                {
                    filenames.RemoveAt(position);
                }
                else
                {
                    Console.WriteLine("In LevelNames.cs could not remove file. File {0} does not exist", oldFilename);
                }
                position = position + 1;
            }
            StorageDevice.BeginShowSelector(PlayerIndex.One, this.saveLevelName, null);
        }

        public void changeFilename(string oldFilename, string newFilename)
        {
            int position = 0;
            foreach (var file in filenames)
            {
                if (oldFilename == file)
                {
                    filenames[position] = newFilename;
                    break;
                }
                position = position + 1;
            }
        }
    }
}
