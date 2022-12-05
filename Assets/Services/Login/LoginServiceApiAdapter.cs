using System;
using Services.Dto;
using Services.Login;
using UnityEngine;

public class LoginServiceApiAdapter : ApiAdapterBase
{
    private const string LoginPath = "/auth/login";
    private const string RegisterPath = "/auth/register";
    private const string Port = ":8080";

    public void Login(string login, string password, EventHandler<LoginResponse> successHandler,
        EventHandler<ErrorResponse> errorHandler)
    {
        var requestBody = JsonUtility.ToJson(new LoginRequest
        {
            login = login,
            password = password
        });
        StartCoroutine(DoRequestCoroutine(endpointAddress + Port + LoginPath, requestBody, Post, successHandler,
            errorHandler));
    }

    public void Register(string login, string password, EventHandler<RegisterResponse> successHandler,
        EventHandler<ErrorResponse> errorHandler)
    {
        var requestBody = JsonUtility.ToJson(new RegisterRequest
        {
            login = login,
            password = password
        });

        StartCoroutine(DoRequestCoroutine(endpointAddress + Port + RegisterPath, requestBody, Post, successHandler,
            errorHandler));
    }
}