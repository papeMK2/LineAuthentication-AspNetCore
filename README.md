# LineAccountExtensions
LineAccountExtensions provides line authentication like twitter, facebook, and other external login.

# How to use

## Add line login
This extension is extension of AuthenticationBuilder.
So, this usage is same of other external login.

Add the Line Account service in the ConfigureServies method in Startup.cs file.
```csharp
services.AddAuthentication()
    .AddLineAccount(options =>
    {
        options.AppId = Configuration["Authentication:Line:AppId"];
        options.AppSecret = Configuration["Authentication:Line:AppSecret"]; ;
    });
```

## Sign in with Line Account

Run your application and access login page.

Line button show under the text "Usage another service to log in"
