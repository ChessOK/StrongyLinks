StrongyLinks
==============

Предоставляет строго-типизированные методы генерации URL для классов `UrlHelper`, `AjaxHelper` и `HtmlHelper`.

Установка StrongyLinks
-------------

Установка с помощью NuGet: `Install-Package StrongyLinks`.

После установки пакета, в файле *~\Views\Web.config* добавьте пространство имен *ChessOk.StrongyLinks*
в секции *system.web.webPages.razor/pages/namespaces*:
```xml
<pages pageBaseType="System.Web.Mvc.WebViewPage">
  <namespaces>
    <add namespace="System.Web.Mvc" />
    <add namespace="System.Web.Mvc.Ajax" />
    <add namespace="System.Web.Mvc.Html" />
    <add namespace="System.Web.Routing" />
    
    // Включить ссылку на StrongyLinks для всех представлений
    <add namespace="ChessOk.StrongyLinks" />
  </namespaces>
  </pages>
```