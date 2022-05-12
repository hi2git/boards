docker-compose  -f "docker-compose.yml" -f "docker-compose.override.yml" -p boards --ansi never up -d --build --force-recreate --remove-orphans

pause