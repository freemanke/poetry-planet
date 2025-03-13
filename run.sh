#########################################################################
# 本地开发运行，传入不同的环境变量，项目使用不同环境下的数据库，默认为：Development
# 可选项为：Development | Staging | Production

ENVIRONMENT_NAME=Development
if [ "$1" ]; then ENVIRONMENT_NAME=$1; fi

dotnet run \
--environment "${ENVIRONMENT_NAME}" \
--project src/PoetryPlanet.Web/PoetryPlanet.Web.csproj
