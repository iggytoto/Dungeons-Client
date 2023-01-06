using System;
using Services.Dto;
using Services.Login;

public class LoginServiceApiAdapter : ApiAdapterBase
{
    private const string LoginPath = "/auth/login";
    private const string RegisterPath = "/auth/register";

    public void Login(
        LoginRequestDto requestDto,
        EventHandler<LoginResponseDto> successHandler,
        EventHandler<ErrorResponseDto> errorHandler)
    {
        StartCoroutine(
            DoRequestCoroutine(
                GetConnectionAddress() + LoginPath,
                SerializeDto(requestDto),
                Post,
                successHandler,
                errorHandler));
    }

    public void Register(
        RegisterRequestDto requestDto,
        EventHandler<RegisterResponseDto> successHandler,
        EventHandler<ErrorResponseDto> errorHandler)
    {
        StartCoroutine(
            DoRequestCoroutine(
                GetConnectionAddress() + RegisterPath,
                SerializeDto(requestDto),
                Post,
                successHandler,
                errorHandler));
    }
}