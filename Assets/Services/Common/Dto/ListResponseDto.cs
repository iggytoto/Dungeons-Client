using System;
using System.Collections.Generic;
using Services.Dto;

namespace Services.Common.Dto
{
    [Serializable]
    public class ListResponseDto<TDto> : ResponseBaseDto
    {
        public List<TDto> items;
    }
}