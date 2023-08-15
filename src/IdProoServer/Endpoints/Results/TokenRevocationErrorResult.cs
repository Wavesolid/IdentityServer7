﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdProoServer.Extensions;
using IdProoServer.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace IdProoServer.Endpoints.Results;

/// <summary>
/// Result for revocation error
/// </summary>
/// <seealso cref="IdProoServer.Hosting.IEndpointResult" />
public class TokenRevocationErrorResult : IEndpointResult
{
    /// <summary>
    /// Gets or sets the error.
    /// </summary>
    /// <value>
    /// The error.
    /// </value>
    public string Error { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenRevocationErrorResult"/> class.
    /// </summary>
    /// <param name="error">The error.</param>
    public TokenRevocationErrorResult(string error)
    {
        Error = error;
    }

    /// <summary>
    /// Executes the result.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns></returns>
    public Task ExecuteAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return context.Response.WriteJsonAsync(new { error = Error });
    }
}