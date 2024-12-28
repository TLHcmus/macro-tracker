docker compose down -v
docker image prune -f
docker rmi mysql:8.0
.\docker-setup