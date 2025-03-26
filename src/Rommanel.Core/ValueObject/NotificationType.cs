
namespace Rommanel.Core.ValueObject
{
    public enum NotificationType
    {
        BadRequest,    // 400 - Erro de validação ou regra de negócio
        NotFound,      // 404 - Recurso não encontrado
        Unauthorized,  // 401 - Falta de autenticação
        Forbidden,     // 403 - Falta de permissão
        ServerError    // 500 - Erro interno inesperado
    }
}
