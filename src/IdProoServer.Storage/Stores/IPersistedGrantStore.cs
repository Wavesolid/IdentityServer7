﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdProoServer.Storage.Stores;

/// <summary>
/// Interface for persisting any type of grant.
/// </summary>
public interface IPersistedGrantStore
{
    /// <summary>
    /// Stores the grant.
    /// </summary>
    /// <param name="grant">The grant.</param>
    /// <returns></returns>
    Task StoreAsync(Models.PersistedGrant grant);

    /// <summary>
    /// Gets the grant.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    Task<Models.PersistedGrant> GetAsync(string key);

    /// <summary>
    /// Gets all grants based on the filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    Task<IEnumerable<Models.PersistedGrant>> GetAllAsync(PersistedGrantFilter filter);

    /// <summary>
    /// Removes the grant by key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    Task RemoveAsync(string key);

    /// <summary>
    /// Removes all grants based on the filter.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    Task RemoveAllAsync(PersistedGrantFilter filter);
}