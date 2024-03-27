# LineAuthentication for ASP.NET Core
LineAuthentication provides [LINE](https://line.me/) authentication like Twitter, Facebook, and other external login. **Supports [LINE Profile+](https://developers.line.biz/ja/docs/partner-docs/line-profile-plus/)**.


## Support Platforms

- .NET 6+



## How to use

It is very easy to use, just call `AddLine()` which is an extension method of `AuthenticationBuilder`. This API style is same of other external login, so you never confuse. 

```csharp
services
    .AddAuthentication()
    .AddLine(options =>
    {
        options.ClientId = Configuration["Authentication:Line:ChannelId"];
        options.ClientSecret = Configuration["Authentication:Line:ChannelSecret"];
    });
```


To access the LINE Profile+, add an authorization scope and extract the data from the JSON payload.


```csharp
services
    .AddAuthentication()
    .AddLine(options =>
    {
        // ...

        // Add authorization scopes
        options.Scope.Add("real_name");
        options.Scope.Add("gender");
        options.Scope.Add("birthdate");
        options.Scope.Add("address");
        options.Scope.Add("phone");
        options.Scope.Add("email");

        // Map JSON payload to claims
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

        // Access entire JSON payload
        options.Events.OnCreatingTicket = context =>
        {
            var json = context.User.GetRawText();
            return Task.CompletedTask;
        };
    });
```



## Installation

```
dotnet add package LineAuthentication
```



## LINE login docs

- [Japanese](https://developers.line.biz/ja/docs/line-login/integrate-line-login/)
- [English](https://developers.line.biz/en/docs/line-login/integrate-line-login/)



## License

This library is provided under [Apache License 2.0](https://opensource.org/licenses/Apache-2.0).



## Authors
- Tsubasa Yoshino (a.k.a [@papeMK2](https://twitter.com/papeMK2))
- Takaaki Suzuki (a.k.a [@xin9le](https://twitter.com/xin9le))

Tsubasa Yoshino is software developer in Tokyo, Japan. Awarded Microsoft MVP (Azure) since October, 2016. He's the original owner of this project.

Takaaki Suzuki is software developer in Fukui, Japan. Awarded Microsoft MVP (C#) since July, 2012. He's a contributer who led the .NET Standard / .NET 5 and LINE Profile+ support.
