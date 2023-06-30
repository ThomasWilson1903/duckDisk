using System.Collections.Generic;
using System.Windows.Documents;

namespace duckDisk.data.api.user.model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<UserRole> Roles { get; set; } = new();
    }
}
