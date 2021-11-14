﻿using System;
using System.Threading.Tasks;
using FlareApi.Entities;

namespace FlareApi.Api.V1.DataAccess
{
    public interface ISessionRepository
    {
        Task<(string, Guid)> CreateSessionAsync(User user);
        Task<(User?, Exception?)> FindUserAsync(string uen, string password);
    }
}