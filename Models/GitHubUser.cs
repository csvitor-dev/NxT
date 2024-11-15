using Newtonsoft.Json;

namespace SalesApp.Models
{
    public class GitHubUser
    {
        [JsonProperty("avatar_url")]
        public string? AvatarUrl { get; set; } = null!;

        [JsonProperty("bio")]
        public string? Bio { get; set; } = null!;

        [JsonProperty("html_url")]
        public string? HtmlUrl { get; set; } = null!;

        [JsonProperty("login")]
        public string? Login { get; set; } = null!;

        [JsonProperty("name")]
        public string? Name { get; set; } = null!;


        public GitHubUser() { }
        public GitHubUser(string avatarUrl, string bio, string htmlUrl, string login, string name)
        {
            AvatarUrl = avatarUrl;
            Bio = bio;
            HtmlUrl = htmlUrl;
            Login = login;
            Name = name;
        }
    }
}
