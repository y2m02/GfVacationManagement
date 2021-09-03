namespace VacationManagerApi.Models.Responses
{
    public class HolidayResponse : BaseResponse
    {
        public string Name { get; set; }
        public int TotalDays { get; set; }
    }
}
