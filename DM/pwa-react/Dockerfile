FROM node:18.12.1-alpine
#ENV NODE_ENV=production

WORKDIR /app

COPY package.json package-lock.json ./

COPY ./src ./src

COPY ./public ./public

RUN npm install  --omit=dev

COPY . .

EXPOSE 3000

RUN npm build

CMD [ "npm", "start" ]