using System;
using Newtonsoft.Json;
using Services.Dto;
using Services.Login;
using UnityEngine;

public class LoginServiceApiAdapter : ApiAdapterBase
{
    private const string LoginPath = "/auth/login";
    private const string RegisterPath = "/auth/register";

    public void Login(string login, string password, EventHandler<LoginResponse> successHandler,
        EventHandler<ErrorResponseDto> errorHandler)
    {
        var requestBody = JsonConvert.SerializeObject(new LoginRequest
        {
            login = login,
            password = password
        });
        StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + LoginPath, requestBody, Post, successHandler,
            errorHandler));
    }

    public void Register(string login, string password, EventHandler<RegisterResponse> successHandler,
        EventHandler<ErrorResponseDto> errorHandler)
    {
        var requestBody = JsonConvert.SerializeObject(new RegisterRequest
        {
            login = login,
            password = password
        });

        StartCoroutine(DoRequestCoroutine(GetConnectionAddress() + RegisterPath, requestBody, Post, successHandler,
            errorHandler));
    }
}