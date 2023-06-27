namespace duckDisk.data.api.user.dto
{
    internal class JwtRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
