using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TargetTapping.Back_end
{
    public class SerializableLevel
    {
        public SerializableLevel(Level levelPassed)
        {
            entityList = new List<List<SerializableEntity>>();
            foreach (var subList in levelPassed.objectList)
            {
                List<TargetTapping.Back_end.SerializableEntity> placeHolder = new List<TargetTapping.Back_end.SerializableEntity>();

                foreach (var entity in subList)
                {
                    SerializableEntity sEntity = new SerializableEntity();
                    sEntity.entitySubName = entity.objectName;
                    sEntity.entityType = entity.shapeType;
                    sEntity.rectangle = entity.rectangle;
                    sEntity.color = entity.color;

                    placeHolder.Add(sEntity);
                }
                entityList.Add(placeHolder);
            }
            currentPosition = levelPassed.currentPosition;
            multiSelect = levelPassed.multiSelect;
            upTime = levelPassed.upTime;
            holdTime = levelPassed.holdTime;
            levelName = levelPassed.levelName;
        }

        public Level constructLevel(ContentManager content, GraphicsDeviceManager graphics)
        {
            Level levelLoaded = new Level();
            List<List<TargetTapping.Back_end.Object>> objectList = new List<List<TargetTapping.Back_end.Object>>();
            foreach (var subList in entityList)
            {
                List<TargetTapping.Back_end.Object> placeHolder = new List<TargetTapping.Back_end.Object>();
                foreach (var entity in subList)
                {
                    Back_end.Object sObject = new Back_end.Object(entity.entityType, entity.entitySubName, entity.rectangle, entity.color, content, graphics);
                    placeHolder.Add(sObject);
                }
                objectList.Add(placeHolder);
            }
            levelLoaded.objectList = objectList;
            levelLoaded.currentPosition = currentPosition;
            levelLoaded.multiSelect = multiSelect;
            levelLoaded.upTime = upTime;
            levelLoaded.holdTime = holdTime;
            levelLoaded.levelName = levelName;
            return levelLoaded;
        }

        public List<List<TargetTapping.Back_end.SerializableEntity>> entityList { get; set; }
        public int currentPosition { get; set; }
        public bool multiSelect { get; set; }
        public int upTime { get; set; }
        public int holdTime { get; set; }
        public string levelName { get; set; }
    }

    public class SerializableEntity
    {
        public SerializableEntity()
        {

        }

        public string entityType { get; set; }
        public string entitySubName { get; set; }
        public Rectangle rectangle { get; set; }
        public Color color { get; set; }
    }
}
