using HeadHunter.Shared;
using Microsoft.AspNetCore.Components;

namespace HeadHunter.Client.Components
{
    public partial class Timeline
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public List<EventData> Events { get; set; }
    }
}