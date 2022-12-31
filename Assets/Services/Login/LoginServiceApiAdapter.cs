using System;
using Services.Dto;
using Services.Login;

public class LoginServiceApiAdapter : ApiAdapterBase
{
    private const string LoginPath = "/auth/login";
    private const string RegisterPath = "/auth/register";

    public void Login(
        LoginRequest request,
        EventHandler<LoginResponse> successHandler,
        EventHandler<ErrorResponseDto> errorHandler)
    {
        StartCoroutine(
            DoRequestCoroutine(
                GetConnectionAddress() + LoginPath,
                SerializeDto(request),
                Post,
                successHandler,
                errorHandler));
    }

    public void Register(
        RegisterRequest request,
        EventHandler<RegisterResponse> successHandler,
        EventHandler<ErrorResponseDto> errorHandler)
    {
        StartCoroutine(
            DoRequestCoroutine(
                GetConnectionAddress() + RegisterPath,
                SerializeDto(request),
                Post,
                successHandler,
                errorHandler));
    }
}