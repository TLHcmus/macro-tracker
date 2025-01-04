# Use the official MySQL image as the base image
FROM mysql:8.0

COPY Properties/DatabaseSetup/init_mysql.sql /docker-entrypoint-initdb.d/

# Set environment variables
ENV MYSQL_ROOT_PASSWORD=123
ENV MYSQL_DATABASE=macro_tracker

EXPOSE 33006