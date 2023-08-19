using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdessoCase.Core.DTOs
{
    public class TravelRequestDto
    {
        public int TravelId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
