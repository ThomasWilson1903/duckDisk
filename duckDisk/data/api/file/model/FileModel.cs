using duckDisk.data.api.user.model;
using System;

namespace duckDisk.data.api.file.model
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Expansion { get; set; } = string.Empty;
        public int Size { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }
        public Boolean IsPublic { get; set; }
        public User User { get; set; } = new();
    }
}
