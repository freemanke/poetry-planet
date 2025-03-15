#####################################################################
# 构建镜像，如果要使用多平台，添加参数 --platform linux/amd64,linux/arm64
# 使用方法：./build_production_image.sh 6.5.5.1 Staging

NC='\033[0m'
GREEN='\033[0;32m'

TAG_NAME="latest"
ASPNETCORE_ENVIRONMENT="Staging"
if [ "$1" ]; then TAG_NAME=$1; fi
if [ "$2" ]; then ASPNETCORE_ENVIRONMENT=$2; fi
IMAGE="home.freemanke.com:60012/freemanke/poetry-planet:$TAG_NAME"

echo -e "${GREEN}=================================================${NC}"
echo -e "${GREEN}开始构建 ${ASPNETCORE_ENVIRONMENT} 镜像 ${IMAGE}...${NC}"
docker buildx create --use --name my-builder 2>/dev/null || true
docker buildx build --push --provenance=false \
--build-arg ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT}" \
--platform linux/amd64 -t "$IMAGE" -f ./Dockerfile .
echo -e "${GREEN}镜像构建完成${NC}"
