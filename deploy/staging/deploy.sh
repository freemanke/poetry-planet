#######################################################################################
# 部署到测试环境

HOST=root@home1
POETRY_PLANET_PATH="/root/poetry-planet-staging"

echo "开始部署到 Staging 环境 ${POETRY_PLANET_PATH}..."
ssh ${HOST} -t "cd ${POETRY_PLANET_PATH}; docker-compose pull; docker-compose down;"
scp -r ./docker-compose.yml ${HOST}:${POETRY_PLANET_PATH}
ssh ${HOST} -t "cd ${POETRY_PLANET_PATH}; docker-compose up -d; docker-compose ps"
echo "部署完成"