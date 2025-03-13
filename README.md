# 诗词星球

## 诗词星球网站

通过 Blazor + dotnet 9.0 + Radzen.Blazor 构建的古诗词在线网站


### 构建镜像
构建并推送镜像前，需要在开发服务器上登录 `harbor`，登录完成后执行命令构建

```bash
docker login home.freemanke.com:60012 -u 'admin' -p 'password'
./images/dotnetsdk/build.sh
./build_image.sh

```

### 部署生产环境

通过执行命令部署到生产环境

```bash
./deploy_producction.sh
```

### 数据库密码存储

通过 `dotnet user-secrets` 存储敏感信息，防止敏感信息通过源代码暴露

```bash
cd ./src/PoetryPlanet.Web
dotnet user-secrets set "MYSQL_ROOT_PASSWORD" "真实数据库密码"

cd ./src/PoetryPlanet.Web.Tests
dotnet user-secrets set "MYSQL_ROOT_PASSWORD" "真实数据库密码"
```

## 诗词星球客户端

通过 AvaloniaUI 构建的跨平台客户端

### 参考文档
- [AvaloniaUI Dependency Injection](https://dev.to/ingvarx/avaloniaui-dependency-injection-4aka)
- [AvaloniaUI 官方文档](https://docs.avaloniaui.net/docs/next/get-started/get-started)
- [Modern.Net-Tutorial](https://github.com/mysteryx93/Modern.Net-Tutorial/blob/main/README.md)
- [awesome-avalonia](https://github.com/AvaloniaCommunity/awesome-avalonia)
- [Semi.Avalonia](https://github.com/irihitech/Semi.Avalonia)

## 常见问题

### 如何处理全局未处理异常，确保页面可响应

[参考文档](https://www.telerik.com/blogs/work-unhandled-exceptions-gracefully-blazor-server-dotnet-6-error-boundaries)