#####################################################################
# 构建镜像
# 使用方法：./build_production_image.sh 6.5.5.1 Staging

TAG_NAME="latest"
ASPNETCORE_ENVIRONMENT="Staging"
if [ "$1" ]; then TAG_NAME=$1; fi
if [ "$2" ]; then ASPNETCORE_ENVIRONMENT=$2; fi
IMAGE="home.freemanke.com:60012/freemanke/poetry-planet:$TAG_NAME"
docker buildx create --use --name my-builder 2>/dev/null || true
docker buildx build --push --provenance=false \
--build-arg ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT}" \
--platform linux/amd64,linux/arm64 -t "$IMAGE" -f ./Dockerfile .
