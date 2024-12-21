using BLL.DAL;

namespace BLL.Services.Bases;

public abstract class ServiceBase
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; } = string.Empty;
    
    protected virtual string OperationFailed => "Operation failed!";

    protected readonly Db _db;

    protected ServiceBase(Db db)
    {
        _db = db;
    }

    public virtual ServiceBase Success(string message = "")
    {
        IsSuccessful = true;
        Message = message;
        return this;
    }

    public virtual ServiceBase Error(string message = "")
    {
        IsSuccessful = false;
        Message = $"{OperationFailed} {message}";
        return this;
    }
}