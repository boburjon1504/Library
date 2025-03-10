﻿using Library.Models.Entities;

namespace Library.API.Services.Interfaces;

public interface IAuthService
{
    ValueTask<User> RegisterAsync(User user, CancellationToken cancellationToken = default);

    ValueTask<string> LoginAsync(User user, CancellationToken cancellationToken = default);
}
