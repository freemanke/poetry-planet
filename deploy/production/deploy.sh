#######################################################################################
# 部署到 Production 环境

POETRY_PLANET_PATH="/root/poetry-planet-production"
echo "开始部署到 Production 环境 ${POETRY_PLANET_PATH}"

ssh root@home2 -t "cd ${POETRY_PLANET_PATH}; docker-compose down;"
scp -r ./docker-compose.yml root@home2:${POETRY_PLANET_PATH}
ssh root@home2 -t "cd ${POETRY_PLANET_PATH};docker-compose pull;docker-compose up -d;docker-compose ps"