#############################################################################
# 通过扩展插件构建基于不同架构的基础构建镜像

HTTP_PROXY=http://127.0.0.1:7890
IMAGE=home.freemanke.com:60012/freemanke/poetry-planet-dotnetsdk:9.0

export http_proxy=${HTTP_PROXY}
export https_proxy=${HTTP_PROXY}
docker buildx create --use --name my-builder 2>/dev/null || true
docker buildx build --push --provenance=false --platform linux/amd64,linux/arm64 -t $IMAGE .



