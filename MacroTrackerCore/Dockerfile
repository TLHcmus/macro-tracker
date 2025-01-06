# Use the official MySQL image as the base image
FROM mysql:8.0

COPY Properties/DatabaseSetup/init_mysql.sql /docker-entrypoint-initdb.d/

EXPOSE 33006