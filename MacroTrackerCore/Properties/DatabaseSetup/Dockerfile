# Use the official MySQL image as the base image
FROM mysql:8.0

COPY Init/init.sql /docker-entrypoint-initdb.d/

# Set environment variables
ENV MYSQL_ROOT_PASSWORD=123

EXPOSE 33006