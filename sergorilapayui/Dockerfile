FROM node:latest AS build
WORKDIR /app
COPY ["sergorilapayui/package*.json", "frontend/"]
WORKDIR /app/frontend
RUN npm install
COPY ["sergorilapayui/", "./"]

EXPOSE 3000
CMD ["npm", "start"]