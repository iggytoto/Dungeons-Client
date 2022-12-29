using Services.Dto;

namespace Services.Common.Dto
{
    public interface IDtoDeserializer<TResponse> where TResponse : ResponseBaseDto
    {
        TResponse Deserialize(string json);
    }
}