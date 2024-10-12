using Microsoft.AspNetCore.Mvc;

namespace Web.Filters;

public class ValidationFilterAttribute : TypeFilterAttribute
{
    public ValidationFilterAttribute()
        : base(typeof(ValidationFilter))
    {}
}