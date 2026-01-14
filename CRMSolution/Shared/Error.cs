namespace Shared;

public class Error
{
    public string Code { get; }

    public string Messege { get; }

    public ErrorType Type { get; }

    public string? InvalidField { get; }

    private Error(string code, string messege, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Messege = messege;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error NotFound(string? code, string? message, Guid? id)
        => new(code ?? "record.not.found", message, ErrorType.NOT_FOUND);

    public static Error Validation(string? code, string message, string? invalidField = null)
        => new(code ?? "value.is.invalid", message, ErrorType.VALIDATION, invalidField);

    public static Error Conflict(string? code, string message)
        => new(code ?? "value.is.confllict", message, ErrorType.CONFLICT);

    public static Error Failure(string? code, string messege)
        => new (code ?? "failure", messege, ErrorType.FAILERULE);

}

public enum ErrorType
{
    /// <summary>
    /// Ошибка с вадидацией
    /// </summary>
    VALIDATION,

    /// <summary>
    /// Ошибка ничего не найдено
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// Ошибка сервера
    /// </summary>
    FAILERULE,

    /// <summary>
    /// Ошибка конфликт
    /// </summary>
    CONFLICT
}