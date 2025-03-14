# 诗词星球

## 诗词星球网站

通过 Blazor + dotnet 9.0 + Radzen.Blazor 构建的古诗词在线网站，实现以下开发最佳实践
1. 全栈一体化开发 Blazor 技术栈
2. 依赖注入
3. 基于 EntityFramework 的 ORM 实现，实现数据库的自动迁移
4. 支持 linux/arm64, linux/amd64 对平台同时构建和发布
5. 自动化单元和集成测试
6. 自动化一键发布和部署
7. Unit Of Work 数据访问模式的实现
8. AOP 实现优雅全局异常处理

### 开发测试和构建

通过 Rider 进行开发

```bash
# 构建基础镜像
docker login home.freemanke.com:60012 -u 'freemanke' -p '***'
./images/dotnetsdk/build.sh

# 本地运行项目，可选参数 [Staging | Production]
./run.sh Development 

# 运行单元和集成测试
./test.sh

# 构建并推送 Staging 镜像
./build_image.sh

# 部署到 Staging 环境
./deploy_Staging.sh
```

### 数据库密码存储

通过 `dotnet user-secrets` 本机存储敏感信息，防止敏感信息通过源代码暴露

```bash
cd ./src/PoetryPlanet.Web
dotnet user-secrets set "MYSQL_ROOT_PASSWORD" "***"

cd ./src/PoetryPlanet.Web.Tests
dotnet user-secrets set "MYSQL_ROOT_PASSWORD" "***"
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

### Blazor 项目中如何处理全局未处理异常
[参考文档](https://www.telerik.com/blogs/work-unhandled-exceptions-gracefully-blazor-server-dotnet-6-error-boundaries)