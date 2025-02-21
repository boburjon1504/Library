using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library.Models.Common.ForEntity;

public class PaginationModel
{
    public int CurrentPage { get; set; } = 1;

    [JsonIgnore]
    public const int PageSize = 10;
}
