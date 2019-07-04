using System;
using Microsoft.AspNetCore.Mvc.Filters;
// using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;



public abstract class ExceptionHandler{

    public ActionExecutingContext _context;
    public ExceptionHandler _next;

    public ExceptionHandler(){

    }
    public ExceptionHandler(ref ActionExecutingContext context){
        _context = context;
        _next = new lastException(ref _context);
    }

    public abstract void HandleException(Exception err);
    public abstract void RegisterNext(ExceptionHandler nextHandler);

}

public class lastException : ExceptionHandler{
    public ActionExecutingContext _context;
    public lastException(ref ActionExecutingContext context){
        _context = context;
    }

    override public void HandleException(Exception err){
        _context.Result = new UnauthorizedObjectResult(new {Status = "Error", Message = "Failed to handle Exception"});
    }

    override public void RegisterNext(ExceptionHandler nextHandler){
        _next = nextHandler;
    }
}

public class HandleArgumentException : ExceptionHandler{

    public HandleArgumentException(ref ActionExecutingContext context): base(ref context){
    }

    override public void HandleException(Exception err){
        if (err.GetType() == typeof(ArgumentException)){
            _context.Result = new UnauthorizedObjectResult(new {Status = "Error", Message = "Failed to parse your base 64 token"});
        }else{
            _next.HandleException(err);
        }

    }

    override public void RegisterNext(ExceptionHandler nextHandler){
        _next = nextHandler;
    }

}

public class TokenExpiredException : ExceptionHandler{
    public TokenExpiredException(ref ActionExecutingContext context): base(ref context){

    }

    override public void HandleException(Exception err){
      
        if (err.GetType() == typeof(SecurityTokenExpiredException)){
            _context.Result = new UnauthorizedObjectResult(new {Status = "Error", Message = "Your token has expired please login again"});
        }else{
            _next.HandleException(err);
        }

    }

    override public void RegisterNext(ExceptionHandler nextHandler){
        _next = nextHandler;
    }
}