// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Security.Claims;

using IdentityModel;

using IdProoServer.Extensions;
using IdProoServer.Services;
using IdProoServer.Storage.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace IdProoServer;

/// <summary>
/// Class for useful helpers for interacting with IdentityServer
/// </summary>
public class IdentityServerTools
{
    public readonly IHttpContextAccessor ContextAccessor;
    private readonly ITokenCreationService _tokenCreation;
    private readonly ISystemClock _clock;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityServerTools" /> class.
    /// </summary>
    /// <param name="contextAccessor">The context accessor.</param>
    /// <param name="tokenCreation">The token creation service.</param>
    /// <param name="clock">The clock.</param>
    public IdentityServerTools(IHttpContextAccessor contextAccessor, ITokenCreationService tokenCreation, ISystemClock clock)
    {
        ContextAccessor = contextAccessor;
        _tokenCreation = tokenCreation;
        _clock = clock;
    }

    /// <summary>
    /// Issues a JWT.
    /// </summary>
    /// <param name="lifetime">The lifetime.</param>
    /// <param name="claims">The claims.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">claims</exception>
    public virtual async Task<string> IssueJwtAsync(int lifetime, IEnumerable<Claim> claims)
    {
        if (claims == null) throw new ArgumentNullException(nameof(claims));

        var issuer = ContextAccessor.HttpContext.GetIdentityServerIssuerUri();

        var token = new Token
        {
            CreationTime = _clock.UtcNow.UtcDateTime,
            Issuer = issuer,
            Lifetime = lifetime,

            Claims = new HashSet<Claim>(claims, new ClaimComparer())
        };

        return await _tokenCreation.CreateTokenAsync(token);
    }

    /// <summary>
    /// Issues a JWT.
    /// </summary>
    /// <param name="lifetime">The lifetime.</param>
    /// <param name="issuer">The issuer.</param>
    /// <param name="claims">The claims.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">claims</exception>
    public virtual async Task<string> IssueJwtAsync(int lifetime, string issuer, IEnumerable<Claim> claims)
    {
        if (String.IsNullOrWhiteSpace(issuer)) throw new ArgumentNullException(nameof(issuer));
        if (claims == null) throw new ArgumentNullException(nameof(claims));

        var token = new Token
        {
            CreationTime = _clock.UtcNow.UtcDateTime,
            Issuer = issuer,
            Lifetime = lifetime,

            Claims = new HashSet<Claim>(claims, new ClaimComparer())
        };

        return await _tokenCreation.CreateTokenAsync(token);
    }
}