StrongyLinks
==============

Provides strongly-typed extensions methods for `UrlHelper`, `AjaxHelper` and `HtmlHelper` classes.

Why use it?
------------
**Pros.**

* Changed action parameter name? There is no need to change the links code.
* Using [MvcBuildViews](http://stackoverflow.com/a/542944/1317575)? Get compile time safety when 
adding new parameters and deleting existing.
* Like Rename refactoring? Use it.
* Always forget controller action names and parameters? No problem.
* You can move controllers to areas without need to add new "area" parameter for links.

**Cons.**
Performance penalty (see below).

Performance
--------------
Simple performance comparison between default helpers and strongly-typed helpers 
give us the following results for URL generation:

* **Parameterless action** — ~1.3x slower (with "area" property set)
* **1 constant parameter** — ~1.3x slower
* **1 complex parameter** — ~2.2x slower

StrongyLinks use CachedExpressionCompiler from Microsoft.Web.Mvc to obtain
parameters values.

Installation
-------------

StrongyLinks is available as a [NuGet package](http://nuget.org/packages/StrongyLinks). To
install it run `Install-Package StrongyLinks` in your Package Manager Console. Once installed, it is ready to use.

Usage
----------

### Supported expressions
StrongyLinks support any kind of expression:

```csharp
// Parameterless 
Url.Action<HomeController>(c => c.Index());

// Constant as a parameter
Url.Action<HomeController>(c => c.Details(3));

// Invocation as a parameter, etc.
Url.Action<HomeController>(c => c.Details(SomeFunc() + 3));
```

All StrongyLinks methods accompany default MVC helper methods. ActionNameAttribute is fully supported.

### UrlHelper methods
```csharp
Url.Action<HomeController>(c => c.Index());
Url.AbsoluteAction<HomeController>(c => c.Index(), "http"); 
```

### HtmlHelper methods
```csharp
// Anchors
Html.ActionLink<HomeController>(c => c.Index());
Html.ActionLink<HomeController>(c => c.Index(), new { @class = "btn" });

// Forms
Html.BeginForm<HomeController>(c => c.Index());
Html.BeginForm<HomeController>(c => c.Index(), FormMethod.Post);
Html.BeginForm<HomeController>(c => c.Index(), FormMethod.Post, new { @class = "form-horizontal" });
```

### AjaxHelper methods
```csharp
// Anchors
Ajax.ActionLink<HomeController>(c => c.Index(), ajaxOptions);
Ajax.ActionLink<HomeController>(c => c.Index(), ajaxOptions, new { @class = "btn" });

// Forms
Ajax.BeginForm<HomeController>(c => c.Index(), FormMethod.Post, ajaxOptions);
Ajax.BeginForm<HomeController>(c => c.Index(), FormMethod.Post, ajaxOptions, new { @class = "form-horizontal" });
```

### Areas
StrongyLinks has `AreaNameAttribute` class. If it is applied to the target controller, the url
will be generated using that area name (it is passed to RouteValueDictionary):

```csharp
[AreaName("SomeArea")]
public class AnotherAreaController { /* Index */ }

// Somewhere in the code
Url.Action<AnotherArea>(c => c.Index()); // '/SomeArea/AnotherArea'
```

### Razor
Since Razor view engine treats angle brackets as beginning of HTML code, you have to
enclose statements in parenthesis:

```
@(Url.Action<HomeController>(c => c.Index()))
