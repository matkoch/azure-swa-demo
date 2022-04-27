using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace SwaApi
{
    // https://docs.microsoft.com/en-us/azure/static-web-apps/user-information?tabs=csharp#api-functions

    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class StaticWebAppsPrincipalAttribute : Attribute
    {
    }

    [Extension("StaticWebAppsPrincipal")]
    public class StaticWebAppsPrincipalConfigProvider : IExtensionConfigProvider
    {
        private readonly IBindingProvider _bindingProvider;

        public StaticWebAppsPrincipalConfigProvider(StaticWebAppsPrincipalBindingProvider bindingProvider)
        {
            _bindingProvider = bindingProvider;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<StaticWebAppsPrincipalAttribute>().Bind(_bindingProvider);
        }
    }

    public class StaticWebAppsPrincipalBindingProvider : IBindingProvider
    {
        private readonly StaticWebAppsPrincipalBinding _binding;

        public StaticWebAppsPrincipalBindingProvider(StaticWebAppsPrincipalBinding binding)
        {
            _binding = binding;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            _binding.Parameter = context.Parameter;
            return Task.FromResult(_binding as IBinding);
        }
    }

    public class StaticWebAppsPrincipalBinding : IBinding
    {
        private readonly StaticWebAppsPrincipalValueProvider _valueProvider;

        public StaticWebAppsPrincipalBinding(StaticWebAppsPrincipalValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public bool FromAttribute { get; }
        public ParameterInfo? Parameter { get; set; }

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            throw new NotImplementedException();
        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            if (Parameter is null)
                throw new ArgumentNullException(nameof(Parameter));

            _valueProvider.Type = Parameter.ParameterType;
            _valueProvider.ParameterName = Parameter.Name;
            return Task.FromResult(_valueProvider as IValueProvider);
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor();
        }
    }

    public class StaticWebAppsPrincipalValueProvider : IValueProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaticWebAppsPrincipalValueProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Type? Type { get; set; }
        public string? ParameterName { get; set; }

        public Task<object> GetValueAsync()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return Task.FromResult<object>(ClientPrincipal.ParseFromRequest(request));
        }

        public string ToInvokeString()
        {
            return string.Empty;
        }
    }

    public class ClientPrincipal
    {
        public string IdentityProvider { get; set; }
        public string UserId { get; set; }
        public string UserDetails { get; set; }
        public IEnumerable<string> UserRoles { get; set; }

        public static Task<ClaimsPrincipal> ParseFromRequest(HttpRequest request)
        {
            var principal = new ClientPrincipal();

            if (request.Headers.TryGetValue("x-ms-client-principal", out var header))
            {
                var data = header[0];
                var decoded = Convert.FromBase64String(data);
                var json = Encoding.UTF8.GetString(decoded);
                principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            principal.UserRoles = principal.UserRoles?.Except(new[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);

            if (!principal.UserRoles?.Any() ?? true)
            {
                return Task.FromResult(new ClaimsPrincipal());
            }

            var identity = new ClaimsIdentity(principal.IdentityProvider);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
            identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
            identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));

            return Task.FromResult(new ClaimsPrincipal(identity));
        }
    }

}
