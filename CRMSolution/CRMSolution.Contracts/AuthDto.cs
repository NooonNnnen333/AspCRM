namespace CRMSolution.Presenters;

public record LoginDto(string Username, string Password);

public record TokenResponseDto(string Token);
