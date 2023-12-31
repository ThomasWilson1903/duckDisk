﻿using duckDisk.data.api.user.model;
using System;

namespace duckDisk.data.api.folder.model
{
    public class FolderModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }
        public Boolean IsPublic { get; set; }
        public User User { get; set; } = new();

        //Не обращаться, когда не надо
        //TODO: Сделать гетер
        private string imageTypeIcon = "\\Resources\\folderIcon.png";

        private string isPublic = "\\Resources\\LinkBorder.png";

        public string getIcon()
        {
            return imageTypeIcon;
        }
        public string getIsPublic()
        {
            return isPublic;
        }
    }
}
