using System;
using System.Collections.Generic;
using Orleans.Indexing;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    [Serializable]
    public class UserProfileState
    {
        public UserProfile Profile { get; set; }
    }

    public class UserState
    {
        public string SubjectId { get; set; }
        public string Email { get; set; }
        public string Password{get; set; }
        
        public string Username { get; set; }
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    }

    [Serializable]
    public class UserProfile
    {
        public string SubjectId { get; set; }
        [ActiveIndex]
        public string Email { get; set; }
        [ActiveIndex]
        public string Phone{get; set; }
        [ActiveIndex]
        public string Username { get; set; }
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
    }
}