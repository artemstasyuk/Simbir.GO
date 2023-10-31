using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Simbir.GO.Server.ApplicationCore.Specifications.Base;


public class BaseFilter
{
    
    [Browsable(false)] [JsonIgnore] public bool LoadChildren { get; set; } = true;

    [Browsable(false)] [JsonIgnore] public bool IsPagingEnabled { get; set; } = true;

    public int Start { get; set; } = 1;
    public int Count { get; set; } = 10;
    
}