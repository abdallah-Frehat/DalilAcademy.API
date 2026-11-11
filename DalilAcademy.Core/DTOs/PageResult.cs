namespace DalilAcademy.Core.DTOs
{
    public record PageResult<T>(List<T> Items, int page, int pageSize, int totalCount )
    {
        public int TotalPages => (int)Math.Ceiling((double)totalCount / pageSize);
    }
}
