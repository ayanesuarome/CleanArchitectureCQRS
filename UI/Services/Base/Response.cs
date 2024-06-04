namespace CleanArch.BlazorUI.Services.Base;

public record Response<T>
{
    public string Message { get; set; } = null!;
    public string ValidationErrors { get; set; } = null!;
    public bool Success { get; set; } = true;
    public T Data { get; set; }
}