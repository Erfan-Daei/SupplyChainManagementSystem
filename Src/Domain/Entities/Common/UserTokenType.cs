namespace Domain.Entities.Common
{
    public enum UserTokenType   //enum for type of UserToken
    {
        EmailConfirmation,
        RefreshToken,
        ChangePasswordConfirmation,
        TepmHashedPassword
    }
}
