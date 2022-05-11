
::docker build -f "D:\Projects\boards\Board.Web\Dockerfile" -t boardweb:dev --target final "D:\Projects\boards"
::docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Docker -v D:\Boards:/usr/share/boards --name Boards boardweb:dev
docker-compose  -f "docker-compose.yml" -f "docker-compose.override.yml" -p boards --ansi never up -d --build --remove-orphans

pause