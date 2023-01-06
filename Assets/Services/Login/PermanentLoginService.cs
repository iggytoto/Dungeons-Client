using System;
using UnityEngine;

/**
 * This is the permanent login service where user id and token can be set up manually for testing purposes;
 */
[Obsolete("Obsolete due AutoReconnectLoginServiceImplementation")]
public class PermanentLoginService : LoginService
{
    [SerializeField] public long userId = 1;
    [SerializeField] public string token = "permanent";

    public new UserContext UserContext => new()
    {
        userId = userId,
        value = token
    };
}