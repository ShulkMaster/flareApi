using System;
using FlareApi.Entities;

namespace FlareApi.Service.Driver
{
    public interface ITokenService
    {
        string ExpediteToken(User user, Guid tokenId);
        string GetUserNameFromToken(string token);
    }
}