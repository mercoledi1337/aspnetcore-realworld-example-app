﻿internal class AuthenticationSettings
{
    public AuthenticationSettings()
    {
    }
    public string JwtKey { get; set; }
    public string JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; }
}