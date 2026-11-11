namespace DalilAcademy.Core.DTOs
{
    public record RegisterDto(
   
        string Email,
        string password,
        string FullName,
        string PhoneNumber,
        string Roles


    );
}