using System;
using JetBrains.Annotations;

namespace Services.Dto
{
    [Serializable]
    public class ResponseBaseDto
    {
        [CanBeNull] public long code;
        [CanBeNull] public string message;
    }
}