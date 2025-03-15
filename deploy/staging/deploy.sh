#######################################################################################
# 部署到 Staging 环境

POETRY_PLANET_PATH="/root/poetry-planet-staging"
echo "开始部署到 Staging 环境 ${POETRY_PLANET_PATH}"

ssh root@home1 -t "cd ${POETRY_PLANET_PATH}; docker-compose down;"
scp -r ./docker-compose.yml root@home1:${POETRY_PLANET_PATH}
ssh root@home1 -t "cd ${POETRY_PLANET_PATH};docker-compose pull;docker-compose up -d;docker-compose ps"