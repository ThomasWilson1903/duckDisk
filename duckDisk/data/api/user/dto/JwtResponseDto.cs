namespace duckDisk.data.api.user.dto
{
    internal class JwtResponseDto
    {
        public string Type { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
