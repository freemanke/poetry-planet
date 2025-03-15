#############################################################################
# 通过扩展插件构建基于不同架构的基础构建镜像

IMAGE=home.freemanke.com:60012/freemanke/mysql:5.7
docker buildx create --use --name my-builder 2>/dev/null || true
docker buildx build --push --provenance=false --platform linux/amd64 -t $IMAGE .



