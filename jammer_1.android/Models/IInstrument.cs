using System;

namespace Jammer_1.Models
{
    interface IInstrument
    {
        string AzureVersion { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        string Genre_id { get; set; }
        string Id { get; set; }
        string Instrument_type_id { get; set; }
        string Skill_rating { get; set; }
        DateTimeOffset Time_playing_instrument { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
        string User_id { get; set; }
    }
}