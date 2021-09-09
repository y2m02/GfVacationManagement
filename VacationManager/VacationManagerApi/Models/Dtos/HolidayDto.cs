namespace VacationManagerApi.Models.Dtos
{
    public class HolidayDto : BaseDto
    {
        public string Name { get; set; }
        public int TotalDays { get; set; }
    }
}
