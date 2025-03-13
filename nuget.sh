#######################################################################################
# 使用华为云拉取 dotnet 组件包，提升速度

dotnet nuget add source -n huawei https://repo.huaweicloud.com/repository/nuget/v3/index.json
dotnet nuget enable source huawei
dotnet nuget disable source nuget.org